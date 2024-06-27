using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.ProductEntity;

namespace LuckyFoodSystem.OrdersDelivery.Bll.Services;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(Guid productId, CancellationToken token = default);
}
