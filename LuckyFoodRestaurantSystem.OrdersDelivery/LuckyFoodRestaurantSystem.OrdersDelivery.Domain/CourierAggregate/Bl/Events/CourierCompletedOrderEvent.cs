using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Events;

public class CourierCompletedOrderEvent : DomainEvent
{
    public OrderId CompletedOrderId { get; private set; }

    public CourierCompletedOrderEvent(CourierId courierId, OrderId orderId)
    {
        AggregateId = courierId.Value;
        CompletedOrderId = orderId;
    }
}
