using LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Events;

public class CourierChangedStatusEvent : DomainEvent
{
    public CourierStatus CourierStatus { get; private set; }

    public CourierChangedStatusEvent(CourierId courierId, CourierStatus courierStatus)
    {
        AggregateId = courierId.Value;
        CourierStatus = courierStatus;
    }
}
