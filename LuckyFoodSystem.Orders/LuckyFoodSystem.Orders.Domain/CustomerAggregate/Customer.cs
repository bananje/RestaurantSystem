using LuckyFoodSystem.Orders.Domain.CustomerAggregate.Bl.Events;
using LuckyFoodSystem.Orders.Domain.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;

namespace LuckyFoodSystem.Orders.Domain.CustomerAggregate;

public class Customer : AggregateRoot<CustomerId>
{
    private readonly HashSet<OrderId> _orders = [];

    public string FirstName { get; private set; } = string.Empty;

    public string MiddleName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public CustomerEmail Email { get; private set; } = null!;

    public CustomerPhone Phone { get; private set; } = null!;

    public int OrdersCount { get; private set; }

    public IReadOnlyCollection<OrderId> Orders => _orders;

    public Address? DeliveryAddress { get; private set; }

    public string FullName => $"{MiddleName} {FirstName} {LastName}";

    public Customer()
    {

    }

    public Customer(string firstName,
                    string middleName,
                    string lastName,
                    CustomerEmail email,
                    CustomerPhone phone,
                    Address address)
    {
        Id = CustomerId.CreateUnique();
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        OrdersCount = 0;
        DeliveryAddress = address;

        RaiseEvent(new CustomerCreatedEvent(
             Id,
             FirstName,
             MiddleName,
             LastName,
             Email,
             Phone,
             OrdersCount,
             DeliveryAddress));
    }

    public void UpdateCustomerInfo(
        CustomerId customerId,
        string firstName,
        string middleName,
        string lastName,
        CustomerEmail email,
        CustomerPhone phone,
        Address address)
    {
        Id = customerId;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        DeliveryAddress = address;
    }

    public void AddOrder(Order order)
    {
        if (order.CurrentStatus.Name == OrderStatus.Complete.Name)
        {
            OrdersCount++;
        }

        _orders.Add(order.Id!);

        RaiseEvent(new AddedOrderEvent(Id!, order.Id!));
    }

    public void RemoveOrder(OrderId orderId)
    {
        _orders.Remove(orderId);

        RaiseEvent(new RemovedOrderEvent(Id!, orderId));
    }

    public void ChangeDeliveryAddress(Address address)
    {
        DeliveryAddress = address;

        RaiseEvent(new DeliveryAddressChangedEvent(Id, address));
    }

    #region Event Sourcing

    protected override void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case CustomerCreatedEvent @e: OnCustomerCreated(@e); break;
            case AddedOrderEvent @e: OnAddedOrder(@e); break;
            case RemovedOrderEvent @e: OnRemovedOrder(@e); break;
            case DeliveryAddressChangedEvent @e: OnDeliveryAddressChanged(@e); break;
        }
    }

    private void OnCustomerCreated(CustomerCreatedEvent @event)
    {
        Id = CustomerId.CreateUnique();
        FirstName = @event.FirstName;
        MiddleName = @event.MiddleName;
        LastName = @event.LastName;
        Email = @event.Email;
        Phone = @event.Phone;
        OrdersCount = @event.OrdersCount;
        DeliveryAddress = @event.DeliveryAddress;
    }

    private void OnAddedOrder(AddedOrderEvent @event)
    {
        Id = CustomerId.Create(@event.AggregateId);
        _orders.Add(@event.OrderId);
    }

    private void OnRemovedOrder(RemovedOrderEvent @event)
    {
        Id = CustomerId.Create(@event.AggregateId);
        _orders.Remove(@event.OrderId);
    }

    private void OnDeliveryAddressChanged(DeliveryAddressChangedEvent @event)
    {
        Id = CustomerId.Create(@event.AggregateId);
        DeliveryAddress = @event.Address;
    }

    #endregion
}
