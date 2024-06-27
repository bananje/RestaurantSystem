using LuckyFoodSystem.Shared.Domain.Features.Converters;
using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.Entity;
using LuckyFoodSystem.Shared.Domain.Models.ValueObjects;
using Newtonsoft.Json;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;

public class CustomerCreatedEvent : DomainEvent
{
    [JsonConverter(typeof(SingleParameterConstructorConverter<CustomerId>))]
    public CustomerId CustomerId { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    [JsonConverter(typeof(SingleParameterConstructorConverter<Email>))]
    public Email Email { get; private set; } = null!;

    [JsonConverter(typeof(SingleParameterConstructorConverter<Phone>))]
    public Phone Phone { get; private set; } = null!;

    public int OrdersCount { get; private set; }

    [JsonConverter(typeof(MultiParameterConstructorConverter<Address>))]
    public Address DeliveryAddress { get; private set; }

    public CustomerCreatedEvent(
            CustomerId customerId,
            string firstName,
            string middleName,
            string lastName,
            Email email,
            Phone phone,
            int ordersCount,
            Address deliveryAddress)
    {
        CustomerId = customerId;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        OrdersCount = ordersCount;
        DeliveryAddress = deliveryAddress;
        AggregateId = CustomerId.Value;
    }
}
