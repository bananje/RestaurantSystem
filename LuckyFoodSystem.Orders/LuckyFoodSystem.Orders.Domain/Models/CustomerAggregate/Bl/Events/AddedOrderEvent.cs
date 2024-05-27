using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Bl.Events;

public class AddedOrderEvent : DomainEvent, ICustomerEvent
{
    public OrderId OrderId { get; private set; }

    public AddedOrderEvent(CustomerId customerId, OrderId orderId)
    {
        AggregateId = customerId.Value;
        OrderId = orderId;
    }
}
