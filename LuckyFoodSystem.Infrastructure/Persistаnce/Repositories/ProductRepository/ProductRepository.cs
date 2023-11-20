using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IImageService _imageService;
        private readonly LuckyFoodDbContext _context;
        public ProductRepository(IHttpContextProvider httpContextProvider,
                                 IImageService imageService,
                                 LuckyFoodDbContext context)
        {
            _httpContextProvider = httpContextProvider;
            _imageService = imageService;
            _context = context;
        }      

        public async Task<Product> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            
        }

        public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProductsByMenuAsync(Guid menuId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddProductAsync(Product product, string rootPath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveProductAsync(Guid procutId, string rootPath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct, string rootPath, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
