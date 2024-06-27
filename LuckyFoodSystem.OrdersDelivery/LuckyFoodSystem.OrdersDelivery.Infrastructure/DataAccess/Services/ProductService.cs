using LuckyFoodSystem.OrdersDelivery.Bll.Services;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.ProductEntity;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Services;

public class ProductService : IProductService
{
    public Task<Product> GetProductByIdAsync(Guid productId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
