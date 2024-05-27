using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity.Enumerations;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;

public class OrderLine : Entity<OrderLineId>
{
    public Product Product { get; private set; } = null!;

    public int Quantity { get; private set; }

    public decimal Price => this.Product.Price.Value * Quantity;

    public decimal Discount => this.Product.Discount.Value;

    public ReadyStatus ReadyStatus { get; private set; } 

    private OrderLine(
        OrderLineId orderLineId,
        Product product,
        ReadyStatus readyStatus,
        int quantity)
    {
        ReadyStatus = readyStatus;
        Product = product;
        this.Quantity = quantity;
    }

    public static OrderLine CreateOrderLine(Product product, int quantity)
    {
        if (product.Status.Name == SaleStatus.UnAvailable.Name)
        {
            throw new BusinessException($"Продукт {product.Title}:Id{product.Id.Value} сейчас недоступен к продаже");
        }

        if (quantity <= 0)
        {
            throw new BusinessException($"Количество является обязательным полем");
        }

        return new OrderLine(OrderLineId.CreateUnique(), product, ReadyStatus.Unready, quantity);
    }

    public void ChangeStatus(ReadyStatus newStatus) => this.ReadyStatus = newStatus;

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new BusinessException($"Количество является обязательным полем");
        }

        this.ReadyStatus = ReadyStatus.Unready;
        Quantity = quantity;
    }
}
