using LuckyFoodSystem.Orders.Bll.Services;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.ProductEntity;

namespace LuckyFoodSystem.Orders.Infrastructure.DataAccess.Services;

public class ProductService : IProductService
{
    public Task<Product> GetProductByIdAsync(Guid productId, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
