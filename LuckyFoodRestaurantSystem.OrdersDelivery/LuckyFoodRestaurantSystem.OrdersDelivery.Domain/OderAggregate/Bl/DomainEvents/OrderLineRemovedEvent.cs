using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

public class OrderLineRemovedEvent : DomainEvent
{
    public OrderLineRemovedEvent(OrderId orderId, OrderLineId newOrderLineId)
    {
        AggregateId = orderId.Value;
        OrderLineId = newOrderLineId;
    }

    public OrderLineId OrderLineId { get; private set; }
}
