using LuckyFoodSystem.Orders.Domain.CourierAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.CourierAggregate.Bl.Events;

public class CourierChangedStatusEvent : DomainEvent, ICourierEvent
{
    public CourierStatus CourierStatus { get; private set; }

    public CourierChangedStatusEvent(CourierId courierId, CourierStatus courierStatus)
    {
        AggregateId = courierId.Value;
        CourierStatus = courierStatus;
    }
}
