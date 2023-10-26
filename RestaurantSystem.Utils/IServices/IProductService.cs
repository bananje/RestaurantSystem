using RestaurantMenu.Models.Models;

namespace RestaurantMenu.Utils.IServices
{
    public interface IProductService
    {
        Task<List<Product>?> GetProductsAsync(int? categoryId);
        Task<IEnumerable<Product>?> GetProductsByMenuAsync(int? menuId, int? key);
        Task<bool> UpsertProductAsync(ProductDTO? product, string webRootPath);
        Task<bool> DeleteProductAsync(int? productId, int? cacheKey, string? webRootPath);
    }
}
