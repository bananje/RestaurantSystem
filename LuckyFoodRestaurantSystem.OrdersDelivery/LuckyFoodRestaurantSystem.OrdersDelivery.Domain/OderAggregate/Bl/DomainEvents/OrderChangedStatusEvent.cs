using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

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
