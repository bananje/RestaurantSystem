using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Events;

public class CourierGatedOrderEvent : DomainEvent
{
    public CourierStatus Status { get; private set; }

    public OrderId CurrentDeliveringOrder { get; private set; }


    public CourierGatedOrderEvent(CourierStatus status, OrderId currentDeliveringOrder, CourierId courierId)
    {
        AggregateId = courierId.Value;
        Status = status;
        CurrentDeliveringOrder = currentDeliveringOrder;
    }
}
