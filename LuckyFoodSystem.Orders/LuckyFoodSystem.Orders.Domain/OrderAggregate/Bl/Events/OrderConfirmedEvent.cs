using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderConfirmedEvent : DomainEvent
{
    public Order Order { get; private set; }

    public OrderConfirmedEvent(Order order)
    {
        AggregateId = order.Id.Value;
        Order = order;
        AggregateVersion = order.Version;
    }
}
