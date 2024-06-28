using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;

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
