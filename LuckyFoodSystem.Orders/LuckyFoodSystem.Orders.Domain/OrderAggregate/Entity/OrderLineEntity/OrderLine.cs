using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.ProductEntity;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;

public class OrderLine : Entity<OrderLineId>
{
    public OrderId OrderId { get; private set; }

    public Product Product { get; private set; } = null!;

    public int Quantity { get; private set; }

    public decimal Price => Product.Price.Value * Quantity;

    public decimal Discount => Product.Discount.Value;

    public ReadyStatus ReadyStatus { get; private set; }

    private OrderLine(
        OrderLineId orderLineId,
        OrderId orderId,
        Product product,
        ReadyStatus readyStatus,
        int quantity)
    {
        OrderId = orderId;
        ReadyStatus = readyStatus;
        Product = product;
        Quantity = quantity;
    }

    public static OrderLine CreateOrderLine(OrderId orderId, Product product, int quantity)
    {
        if (product.Status.Name == SaleStatus.UnAvailable.Name)
        {
            throw new BusinessException($"Продукт {product.Title}:Id{product.Id.Value} сейчас недоступен к продаже");
        }

        if (quantity <= 0)
        {
            throw new BusinessException($"Количество является обязательным полем");
        }

        return new OrderLine(OrderLineId.CreateUnique(), orderId, product, ReadyStatus.Unready, quantity);
    }

    public void ChangeStatus(ReadyStatus newStatus) => ReadyStatus = newStatus;

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new BusinessException($"Количество является обязательным полем");
        }

        ReadyStatus = ReadyStatus.Unready;
        Quantity = quantity;
    }
}
