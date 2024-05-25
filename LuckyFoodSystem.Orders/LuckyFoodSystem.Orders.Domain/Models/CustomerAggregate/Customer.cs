using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate;

public class Customer : AggregateRoot<CustomerId>
{
    private readonly HashSet<OrderId> _orders = new();

    public string FirstName { get; private set; } = null!;

    public string MiddleName { get; private set; } = null!;

    public string LastName { get; private set; } = string.Empty;

    public CustomerEmail Email { get; private set; } = null!;

    public CustomerPhone Phone { get; private set; } = null!;

    public int OrdersCount { get; private set; }

    public IReadOnlyCollection<OrderId> Orders => _orders;

    public Address? DeliveryAddress { get; private set; }

    public string FullName => $"{MiddleName} {FirstName} {LastName}";

    private Customer(CustomerId customerId,
                     string firstName,
                     string middleName,
                     string lastName,
                     CustomerEmail email,
                     CustomerPhone phone,
                     int ordersCount)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        OrdersCount = ordersCount;
    }

    public static Customer CreateCustomer(
            string firstName,
            string middleName,
            string lastName,
            CustomerEmail email,
            CustomerPhone phone,
            int ordersCount)
    
        => new(CustomerId.CreateUnique(), firstName, middleName, lastName, email, phone, ordersCount);

    public void AddOrder(Order order)
    {
        if (order.OrderStatus.Name == OrderStatus.Complete.Name)
        {
            OrdersCount++;
        }

        _orders.Add(order.Id);
    }

    public void RemoveOrder(OrderId orderId)
    {
        _orders.Remove(orderId);
    }

    protected override void Apply(DomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
