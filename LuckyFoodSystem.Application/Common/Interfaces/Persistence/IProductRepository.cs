using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsByMenuAsync(MenuId menuId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
        Product GetProductById(ProductId productId, CancellationToken cancellationToken = default);
        Task<bool> RemoveProductAsync(ProductId productId, string rootPath, CancellationToken cancellationToken = default);
        Task AddProductAsync(Product product, string rootPath, CancellationToken cancellationToken = default);
        Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct,
                                         string rootPath, CancellationToken cancellationToken = default, List<Guid> imageIds = null!);
        
    }
}
