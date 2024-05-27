using LuckyFoodSystem.Orders.Domain.CourierAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Shared.Domain.Models;


namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class CourierAppointedEvent : DomainEvent, IOrderEvent
{
    public CourierId CourierId { get; private set; }

    public CourierAppointedEvent(OrderId orderId, CourierId courierId)
    {
        AggregateId = orderId.Value;
        CourierId = courierId;
    }
}
