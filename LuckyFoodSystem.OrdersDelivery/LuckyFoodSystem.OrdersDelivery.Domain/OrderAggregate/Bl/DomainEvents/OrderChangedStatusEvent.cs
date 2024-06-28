using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;

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
