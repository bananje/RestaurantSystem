using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CustomerAggregate.Bl.Events;

public class RemovedOrderEvent : DomainEvent
{
    public OrderId OrderId { get; private set; }

    public RemovedOrderEvent(CustomerId customerId, OrderId orderId)
    {
        AggregateId = customerId.Value;
        OrderId = orderId;
    }
}
