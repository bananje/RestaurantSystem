using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository;
using LuckyFoodSystem.Infrastructure.Services.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
       
        public Product GetProductById(ProductId productId, CancellationToken cancellationToken = default)
        {
            string key = $"{_cacheKey}:{productId.Value}";
            Product? product;

            string? cachedProduct = _distributedCache.GetString(key);
            if (string.IsNullOrEmpty(cachedProduct))
            {
                product = _decorated.GetProductById(productId, cancellationToken);

                if (product is null) return product!;

                 _distributedCache.SetString(
                    key,
                    JsonConvert.SerializeObject(product, new MenuConverter()));

                return product;
            }

            product = JsonConvert.DeserializeObject<Product>(
                cachedProduct,
                new JsonSerializerSettings
                {
                    ConstructorHandling =
                        ConstructorHandling.AllowNonPublicDefaultConstructor,
                    ContractResolver = new PrivateResolver(),
                    Converters = { new MenuConverter() }
                });

            return product!;
        }

        public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByMenuAsync(MenuId menuId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveProductAsync(ProductId productId, string rootPath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddProductAsync(Product product, string rootPath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct, string rootPath, CancellationToken cancellationToken = default, List<Guid> imageIds = null)
        {
            throw new NotImplementedException();
        }
    }
}
