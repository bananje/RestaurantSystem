using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Events;

public class CourierCompletedOrderEvent : DomainEvent
{
    public OrderId CompletedOrderId { get; private set; }

    public CourierCompletedOrderEvent(CourierId courierId, OrderId orderId)
    {
        AggregateId = courierId.Value;
        CompletedOrderId = orderId;
    }
}
