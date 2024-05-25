using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate;
using LuckyFoodSystem.Shared.Domain.Models;


namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;

public class CourierAppointedEvent : DomainEvent
{
    public CourierId CourierId { get; private set; }

    public CourierAppointedEvent(CourierId courierId)
    {
        CourierId = courierId;
    }
}
