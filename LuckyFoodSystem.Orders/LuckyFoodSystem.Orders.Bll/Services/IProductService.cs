using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.ProductEntity;

namespace LuckyFoodSystem.Orders.Bll.Services;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(Guid productId, CancellationToken token = default);
}
