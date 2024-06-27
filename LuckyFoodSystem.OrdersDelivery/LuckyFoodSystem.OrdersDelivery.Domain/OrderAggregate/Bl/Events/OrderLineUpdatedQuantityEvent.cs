using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;

public class OrderLineUpdatedQuantityEvent : DomainEvent
{
    public OrderLineId OrderLineId { get; private set; }

    public int Quantity { get; private set; }

    public OrderId OrderId { get; private set; }

    public OrderLineUpdatedQuantityEvent(OrderId orderId, OrderLineId orderLineId, int quantity)
    {
        AggregateId = orderId.Value;
        OrderId  = orderId;
        OrderLineId = orderLineId;
        Quantity = quantity;
    }
}
