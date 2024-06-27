using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;

public class OrderLineRemovedEvent : DomainEvent
{
    public OrderLineRemovedEvent(OrderId orderId, OrderLineId newOrderLineId)
    {
        AggregateId = orderId.Value;
        OrderLineId = newOrderLineId;
    }

    public OrderLineId OrderLineId { get; private set; }
}
