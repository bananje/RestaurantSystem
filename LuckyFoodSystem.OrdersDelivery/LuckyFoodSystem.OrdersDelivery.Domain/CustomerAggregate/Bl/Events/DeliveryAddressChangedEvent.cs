using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.Entity;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;

public class DeliveryAddressChangedEvent : DomainEvent
{
    public Address Address { get; private set; }

    public DeliveryAddressChangedEvent(CustomerId customerId, Address address)
    {
        AggregateId = customerId.Value;
        Address = address;
    }
}
