using LuckyFoodSystem.Orders.Domain.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;

public class CustomerCreatedEvent : DomainEvent
{
    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public CustomerEmail Email { get; private set; } = null!;

    public CustomerPhone Phone { get; private set; } = null!;

    public int OrdersCount { get; private set; }

    public Address? DeliveryAddress { get; private set; }

    public CustomerCreatedEvent(
            CustomerId customerId,
            string firstName,
            string middleName,
            string lastName,
            CustomerEmail email,
            CustomerPhone phone,
            int ordersCount)
    {
        AggregateId = customerId.Value;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        OrdersCount = ordersCount;
    }
}
