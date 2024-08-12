using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

public class OrderLineUpdatedQuantityEvent : DomainEvent
{
    public OrderLineId OrderLineId { get; private set; }

    public int Quantity { get; private set; }

    public OrderId OrderId { get; private set; }

    public OrderLineUpdatedQuantityEvent(OrderId orderId, OrderLineId orderLineId, int quantity)
    {
        AggregateId = orderId.Value;
        OrderId = orderId;
        OrderLineId = orderLineId;
        Quantity = quantity;
    }
}
