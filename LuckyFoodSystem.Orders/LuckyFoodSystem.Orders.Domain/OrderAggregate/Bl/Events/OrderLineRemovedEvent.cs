using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderLineRemovedEvent : DomainEvent
{
    public OrderLineRemovedEvent(OrderId orderId, OrderLineId newOrderLineId)
    {
        AggregateId = orderId.Value;
        OrderLineId = newOrderLineId;
    }

    public OrderLineId OrderLineId { get; private set; }
}
