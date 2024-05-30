using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;

public class AddedOrderEvent : DomainEvent
{ 
    public OrderId OrderId { get; private set; }

    public AddedOrderEvent(CustomerId customerId, OrderId orderId)
    {
        AggregateId = customerId.Value;
        OrderId = orderId;
    }
}
