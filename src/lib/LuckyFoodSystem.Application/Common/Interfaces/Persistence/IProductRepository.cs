using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);
        Task<List<Product>> GetProductsByMenuAsync(MenuId menuId, CancellationToken cancellationToken);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync(ProductId productId, CancellationToken cancellationToken);
        Task<bool> RemoveProductAsync(ProductId productId, string rootPath, CancellationToken cancellationToken);
        Task AddProductAsync(Product product, List<Guid> menusIds, string rootPath, CancellationToken cancellationToken);
        Task<Product> UpdateProductAsync(ProductId productId, Product updatedProduct,
                                         string rootPath, CancellationToken cancellationToken,
                                         List<Guid> imageIds = null!, List<Guid> menuAddingIds = null!, List<Guid> menuDeletingIds = null!);
        
    }
}
