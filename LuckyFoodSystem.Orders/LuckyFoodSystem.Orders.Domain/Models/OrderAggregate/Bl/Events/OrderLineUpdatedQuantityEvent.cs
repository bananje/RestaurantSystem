using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;

public class OrderLineUpdatedQuantityEvent : DomainEvent, IOrderEvent
{
    public OrderLineId OrderLineId { get; private set; }
    
    public int Quantity { get; private set; }

    public OrderLineUpdatedQuantityEvent(OrderId orderId, OrderLineId orderLineId, int quantity)
    {
        AggregateId = orderId.Value;
        OrderLineId = orderLineId;
        Quantity = quantity;    
    }
}
