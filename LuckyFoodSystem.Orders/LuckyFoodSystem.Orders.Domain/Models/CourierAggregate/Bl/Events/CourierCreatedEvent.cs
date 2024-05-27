using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Bl.Events;

public class CourierCreatedEvent : DomainEvent, ICourierEvent
{
    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public CourierEmail Email { get; private set; } = null!;

    public CourierPhone Phone { get; private set; } = null!;

    public CourierStatus Status { get; private set; } = null!;

    public CourierCreatedEvent(
        CourierId courierId,
        string firstName,
        string middleName,
        string lastName,
        CourierEmail email,
        CourierPhone phone,
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
