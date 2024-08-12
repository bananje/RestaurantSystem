using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CustomerAggregate;
using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.Entity;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

public class OrderCreatedEvent : DomainEvent
{
    public OrderId OrderId { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public Address Address { get; private set; }

    public OrderCreatedEvent(
        OrderId orderId,
        CustomerId customerId,
        Address address)
    {
        AggregateId = orderId.Value;
        OrderId = orderId;
        CustomerId = customerId;
        Address = address;
    }
}
