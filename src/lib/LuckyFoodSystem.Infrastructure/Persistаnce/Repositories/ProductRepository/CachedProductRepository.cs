using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Constants;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Infrastructure.Services.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.ProductRepository
{
    public class CachedProductRepository : IProductRepository
    {
        private readonly IProductRepository _decorated;
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        private string _cacheKey = $"product{CacheSettings.ObjKey}";
        public CachedProductRepository(IProductRepository productRepository,
                                       IDistributedCache distributedCache,
                                       IConfiguration configuration)
        {
            _configuration = configuration;
            _decorated = productRepository;
            _distributedCache = distributedCache;
        }
        
        public async Task<Product> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken)
        {
            string key = $"{_cacheKey}:{productId.Value}";
            Product? product;

            string? cachedProduct = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(cachedProduct))
            {
                product = await _decorated.GetProductByIdAsync(productId, cancellationToken);

                if (product is null) return product!;

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(product, new ProductConverter()),
                    cancellationToken);

                return product;
            }

            product = JsonConvert.DeserializeObject<Product>(
                cachedProduct,
                new JsonSerializerSettings
                {
                    ConstructorHandling =
                        ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver(),
                    Converters = { new ProductConverter() }
                });

            return product!;
        }

        public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var productsList = await GetProductCollectionFromCache(cancellationToken);

            if (productsList is null || productsList.Count() is 0)
            {
                productsList = await _decorated.GetProductsAsync(cancellationToken);
                await SetProductCollectionToCache(productsList, cancellationToken);
            }

            return productsList;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var productsByCategoryNameList = await GetProductCollectionFromCache(cancellationToken, categoryId);

            if (productsByCategoryNameList is null || productsByCategoryNameList.Count() is 0)
            {
                productsByCategoryNameList = await _decorated.GetProductsByCategoryAsync(categoryId, cancellationToken);
                await SetProductCollectionToCache(productsByCategoryNameList, cancellationToken);
            }

            return productsByCategoryNameList;
        }

        public async Task<List<Product>> GetProductsByMenuAsync(MenuId menuId, CancellationToken cancellationToken)
        {
            var productsByMenuIdList = await GetProductCollectionFromCache(cancellationToken,
                                                                           menuId: menuId.Value.ToString()!);

            if (productsByMenuIdList is null || productsByMenuIdList.Count() is 0)
            {
                productsByMenuIdList = await _decorated.GetProductsByMenuAsync(menuId, cancellationToken);
                await SetProductCollectionToCache(productsByMenuIdList, cancellationToken);
            }

            return productsByMenuIdList;
        }

        public async Task AddProductAsync(Product product, List<Guid> menusIds, string rootPath, CancellationToken cancellationToken)
        {
            if (product is not null)
            {
                await _decorated.AddProductAsync(product, menusIds, rootPath, cancellationToken);

                string key = $"{_cacheKey}:{product.Id.Value}";
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(product, new ProductConverter()),
                    cancellationToken);
            }
        }

        public async Task<bool> RemoveProductAsync(ProductId productId, string rootPath, CancellationToken cancellationToken)
        {
            bool result = await _decorated.RemoveProductAsync(productId, rootPath, cancellationToken);

            if (result)
            {
                string key = $"{_cacheKey}:{productId.Value}";
                string? cachedProduct = await _distributedCache.GetStringAsync(key, cancellationToken);

                if (!string.IsNullOrEmpty(cachedProduct))
                    await _distributedCache.RemoveAsync(key, cancellationToken);

                return true;
            }

            return false;
        }

        public async Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct, string rootPath, 
                                                CancellationToken cancellationToken, 
                                                List<Guid> imageIds = null!, List<Guid> menuAddingIds = null!, List<Guid> menuDeletingIds = null!)
        {
            if (updatedProduct is not null)
            {
                updatedProduct = await _decorated.UpdateProductAsync(productId, updatedProduct, rootPath, cancellationToken, imageIds, menuAddingIds, menuDeletingIds);

                string key = $"{_cacheKey}:{productId.Value}";
                await _distributedCache.SetStringAsync(
                                key,
                                JsonConvert.SerializeObject(updatedProduct, new ProductConverter()),
                                cancellationToken);
            }

            return updatedProduct!;
        }

        private async Task<List<Product>> GetProductCollectionFromCache(CancellationToken cancellationToken, 
                                                                        int categoryId = 0, string menuId = null!)
        {
            var redis = ConnectionMultiplexer
               .Connect(_configuration.GetConnectionString(ConnectionNames.Redis)!);

            var keys = redis.GetServer(_configuration.GetConnectionString(ConnectionNames.Redis)!)
                            .Keys(pattern: $"{_cacheKey}*");

            var productsList = new List<Product>();
            if (keys.Count() is not 0)
            {
                foreach (var key in keys)
                {
                    string? value = await _distributedCache.GetStringAsync(key!, cancellationToken);

                    Product product = JsonConvert.DeserializeObject<Product>(
                        value!,
                        new JsonSerializerSettings
                        {
                            ConstructorHandling =
                                ConstructorHandling.AllowNonPublicDefaultConstructor,
                            ContractResolver = new PrivateResolver(),
                            Converters = { new ProductConverter() }
                        })!;

                    productsList.Add(product);
                }

                if (categoryId is not 0)
                {
                    if (productsList is not null || productsList!.Count() is not 0)
                        productsList = productsList!
                            .Where(u => u.Category.Name == Category.FromId(categoryId).Name).ToList();
                }

                if(menuId is not null)
                {
                    if (productsList is not null || productsList!.Count() is not 0)
                        productsList = productsList!
                            .Where(u => u.Menus.Any(u => u.Id == MenuId.Create(Guid.Parse(menuId)))).ToList();
                }
            }

            return productsList!;
        }
        private async Task SetProductCollectionToCache(List<Product> products, CancellationToken cancellationToken)
        {
            if (products.Count() is not 0 || products is not null)
            {
                foreach (var product in products)
                {
                    string key = $"{_cacheKey}:{product.Id.Value}";

                    await _distributedCache.SetStringAsync(
                        key,
                        JsonConvert.SerializeObject(product, new ProductConverter()),
                        cancellationToken);
                }
            }
            return;
        }
    }
}
