using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderChangedStatusEvent : DomainEvent
{
    public OrderStatus OrderStatus { get; private set; }

    public OrderId OrderId { get; private set; }

    public OrderChangedStatusEvent(OrderId orderId, OrderStatus orderStatus)
    {
        AggregateId = orderId.Value;
        OrderId = orderId;
        OrderStatus = orderStatus;
    }
}
