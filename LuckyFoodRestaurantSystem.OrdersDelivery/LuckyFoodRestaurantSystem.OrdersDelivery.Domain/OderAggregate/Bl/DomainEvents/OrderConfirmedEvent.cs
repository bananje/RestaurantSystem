using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

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
