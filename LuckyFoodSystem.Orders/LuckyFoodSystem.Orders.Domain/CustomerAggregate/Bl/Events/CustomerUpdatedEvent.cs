using LuckyFoodSystem.Orders.Domain.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Features.Converters;
using LuckyFoodSystem.Shared.Domain.Models;
using Newtonsoft.Json;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;

public class CustomerUpdatedEvent : DomainEvent
{
    public CustomerUpdatedEvent(
        CustomerId customerId,
        string firstName,
        string middleName,
        string lastName,
        CustomerEmail email,
        CustomerPhone phone,
        Address deliveryAddress)
    {
        CustomerId = customerId;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        DeliveryAddress = deliveryAddress;
        AggregateId = CustomerId.Value;
    }

    [JsonConverter(typeof(SingleParameterConstructorConverter<CustomerId>))]
    public CustomerId CustomerId { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    [JsonConverter(typeof(SingleParameterConstructorConverter<CustomerEmail>))]
    public CustomerEmail Email { get; private set; } = null!;

    [JsonConverter(typeof(SingleParameterConstructorConverter<CustomerPhone>))]
    public CustomerPhone Phone { get; private set; } = null!;

    [JsonConverter(typeof(MultiParameterConstructorConverter<Address>))]
    public Address DeliveryAddress { get; private set; }
}
