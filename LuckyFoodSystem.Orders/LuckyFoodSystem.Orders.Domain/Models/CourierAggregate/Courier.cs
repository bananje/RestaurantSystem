using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Bl.Events;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Bl.Rules;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.CourierAggregate;

public class Courier : AggregateRoot<CourierId, ICourierEvent>
{
    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public CourierEmail Email { get; private set; } = null!;

    public CourierPhone Phone { get; private set; } = null!;

    public CourierStatus Status { get; private set; } = null!;

    public string FullName => $"{MiddleName} {FirstName} {LastName}";

    public Courier()
    {
        
    }

    public Courier(
        string firstName,
        string middleName,
        string lastName,
        CourierEmail email,
        CourierPhone phone,
        CourierStatus status)
    {
        Id = CourierId.CreateUnique();
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Status = status;

        RaiseEvent(new CourierCreatedEvent(
            Id,
            FirstName,
            MiddleName,
            LastName,
            Email,
            Phone,
            Status));
    }

    public void ChangeStatus(CourierStatus newStatus)
    {
        CheckRule(new CourierStatusMayNotBeChangedWhenCurrentStatusDelivering(Status, newStatus));

        Status = newStatus;

        RaiseEvent(new CourierChangedStatusEvent(Id, newStatus));
    }

    #region Event Sourcing

    protected override void Apply(ICourierEvent @event)
    {
        switch (@event)
        {
            case CourierCreatedEvent @e: OnCourierCreated(@e); break;
            case CourierChangedStatusEvent @e: OnCourierChangedStatus(@e); break;
        }
    }

    private void OnCourierCreated(CourierCreatedEvent @event)
    {
        Id = CourierId.Create(@event.AggregateId);
        FirstName = @event.FirstName;
        MiddleName = @event.MiddleName;
        LastName = @event.LastName;
        Email = @event.Email;
        Phone = @event.Phone;
        Status = @event.Status;
    }

    private void OnCourierChangedStatus(CourierChangedStatusEvent @event)
    {
        Id = CourierId.Create(@event.AggregateId);
        Status = @event.CourierStatus;
    }

    #endregion
}
