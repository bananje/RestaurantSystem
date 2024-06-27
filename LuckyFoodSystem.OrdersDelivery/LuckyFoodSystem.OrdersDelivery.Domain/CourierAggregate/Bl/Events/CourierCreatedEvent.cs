using LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.ValueObjects;

namespace LuckyFoodSystem.OrdersDelivery.Domain.CourierAggregate.Bl.Events;

public class CourierCreatedEvent : DomainEvent
{
    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public Email Email { get; private set; } = null!;

    public Phone Phone { get; private set; } = null!;

    public CourierStatus Status { get; private set; } = null!;

    public CourierCreatedEvent(
        CourierId courierId,
        string firstName,
        string middleName,
        string lastName,
        Email email,
        Phone phone,
        CourierStatus status)
    {
        AggregateId = courierId.Value;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Status = status;
    }
}
