using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

public class OrderClosedEvent : DomainEvent
{
    public DateTime OrderStatusChangedAt { get; private set; }

    public bool IsClosed { get; private set; }

    public OrderStatus CurrentStatus { get; private set; }

    public OrderStatus ClosedWithStatus { get; private set; }

    public OrderId OrderId { get; private set; }

    public OrderClosedEvent(
        OrderId orderId,
        OrderStatus currentStatus,
        OrderStatus closedWithStatus,
        bool isClosed,
        DateTime orderStatusChangedAt)
    {
        AggregateId = orderId.Value;
        OrderId = orderId;
        OrderStatusChangedAt = orderStatusChangedAt;
        IsClosed = isClosed;
        CurrentStatus = currentStatus;
        ClosedWithStatus = closedWithStatus;
    }
}
