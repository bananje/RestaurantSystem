using LuckyFoodSystem.Orders.Domain.CourierAggregate.Bl.Events;
using LuckyFoodSystem.Orders.Domain.CourierAggregate.Bl.Rules;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodSystem.Orders.Domain.CourierAggregate;

public class Courier : AggregateRoot<CourierId>
{
    private readonly HashSet<OrderId> _completeOrders = [];

    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public CourierEmail Email { get; private set; } = null!;

    public CourierPhone Phone { get; private set; } = null!;

    public CourierStatus Status { get; private set; } = CourierStatus.Inactive;

    public OrderId CurrentDeliveringOrder { get; private set; } = null!;

    public IReadOnlyCollection<OrderId> CompleteOrders => _completeOrders;

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

    public void GetOrder(OrderId orderId, OrderStatus orderStatus)
    {
        CheckRule(new OrderStatusMustHaveCompleteStatusOnly(orderStatus));

        Status = CourierStatus.Delivering;
        CurrentDeliveringOrder = orderId;

        RaiseEvent(new CourierGatedOrderEvent(Status, CurrentDeliveringOrder, Id!));
    }

    public void AddCompletedOrder(Order order)
    {
        CheckRule(new OrderMustHaveDeliveredStatus(order.CurrentStatus));

        _completeOrders.Add(order.Id!);

        RaiseEvent(new CourierCompletedOrderEvent(Id!, order.Id!));
    }

    public void ChangeStatus(CourierStatus newStatus)
    {
        CheckRule(new CourierStatusMayNotBeChangedWhenCurrentStatusDelivering(Status, newStatus));

        Status = newStatus;

        RaiseEvent(new CourierChangedStatusEvent(Id, newStatus));
    }

    #region Event Sourcing

    protected override void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case CourierCreatedEvent @e: OnCourierCreated(@e); break;
            case CourierChangedStatusEvent @e: OnCourierChangedStatus(@e); break;
            case CourierGatedOrderEvent @e: OnCouirerGettedOrder(@e); break;
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

    private void OnCouirerGettedOrder(CourierGatedOrderEvent @event)
    {
        Id = CourierId.Create(@event.AggregateId);
        Status = @event.Status;
        CurrentDeliveringOrder = @event.CurrentDeliveringOrder;
    }

    private void OnCourierChangedStatus(CourierChangedStatusEvent @event)
    {
        Id = CourierId.Create(@event.AggregateId);
        Status = @event.CourierStatus;
    }

    #endregion
}
