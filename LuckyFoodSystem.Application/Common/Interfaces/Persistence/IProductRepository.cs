using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsByMenuAsync(Guid menuId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<Product> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
        Task<bool> RemoveProductAsync(Guid procutId, string rootPath, CancellationToken cancellationToken = default);
        Task AddProductAsync(Product product, string rootPath, CancellationToken cancellationToken = default);
        Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct,
                                         string rootPath, CancellationToken cancellationToken = default);
        
    }
}
