using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.CourierAggregate.Bl.Events;

public class CourierGetedOrderEvent : DomainEvent
{
    public CourierStatus Status { get; private set; } = CourierStatus.Inactive;

    public OrderId CurrentDeliveringOrder { get; private set; }

    public CourierGetedOrderEvent(CourierStatus status, OrderId currentDeliveringOrder)
    {
        Status = status;
        CurrentDeliveringOrder = currentDeliveringOrder;
    }
}
