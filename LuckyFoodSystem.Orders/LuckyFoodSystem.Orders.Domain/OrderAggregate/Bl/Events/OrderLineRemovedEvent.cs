using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderLineRemovedEvent : DomainEvent, IOrderEvent
{
    public OrderLineRemovedEvent(OrderId orderId, OrderLineId newOrderLineId)
    {
        AggregateId = orderId.Value;
        OrderLineId = newOrderLineId;
    }

    public OrderLineId OrderLineId { get; private set; }
}
