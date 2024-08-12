using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

public class OrderLineAddedEvent : DomainEvent
{
    public OrderLineAddedEvent(OrderId orderId, OrderLine newOrderLine)
    {
        AggregateId = orderId.Value;
        OrderLine = newOrderLine;
    }

    public OrderId OrderId { get; private set; } = null!;

    public OrderLine OrderLine { get; private set; }
}
