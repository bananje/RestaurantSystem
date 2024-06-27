using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;

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
