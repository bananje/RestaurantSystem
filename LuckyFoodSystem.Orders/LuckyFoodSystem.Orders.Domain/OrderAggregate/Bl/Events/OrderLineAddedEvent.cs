using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderLineAddedEvent : DomainEvent
{
    public OrderLineAddedEvent(OrderId orderId, OrderLine newOrderLine)
    {
        AggregateId = orderId.Value;
        OrderLine = newOrderLine;
    }

    public OrderId OrderId { get; private set; }

    public OrderLine OrderLine { get; private set; }
}
