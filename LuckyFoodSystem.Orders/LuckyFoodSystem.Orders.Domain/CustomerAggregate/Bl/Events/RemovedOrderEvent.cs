using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;

public class RemovedOrderEvent : DomainEvent
{
    public OrderId OrderId { get; private set; }

    public RemovedOrderEvent(CustomerId customerId, OrderId orderId)
    {
        AggregateId = customerId.Value;
        OrderId = orderId;
    }
}
