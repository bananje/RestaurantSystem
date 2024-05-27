using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderLineAddedEvent : DomainEvent, IOrderEvent
{
    public OrderLineAddedEvent(OrderId orderId, OrderLine newOrderLine)
    {
        AggregateId = orderId.Value;
        OrderLine = newOrderLine;
    }

    public OrderLine OrderLine { get; private set; }
}
