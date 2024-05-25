using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Rules;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate;

public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<OrderLine> _orderLines = new();

    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Accepted;

    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Unpaid;

    public CustomerId CustomerId { get; private set; } = null!;

    public CourierId CourierId { get; private set; } = null!;

    public decimal TotalPrice => GetTotalPrice();

    public Address DeliveryAddress { get; private set; } = null!;

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    public Order(
        OrderId orderId,
        CustomerId customerId,
        Address deliveryAddress,
        PaymentStatus paymentStatus,
        OrderStatus orderStatus)
    {
        CheckRule(new TheCustomerMustHaveAnAddress(deliveryAddress));

        Id = OrderId.CreateUnique();
        CustomerId = customerId;
        DeliveryAddress = deliveryAddress;

        RaiseEvent(new OrderCreatedEvent(
            orderId: Id,
            orderStatus: OrderStatus,
            paymentStatus: PaymentStatus,
            customerId: CustomerId,
            deliveryAddress: DeliveryAddress,
            totalPrice: TotalPrice));   
    }

    private Order(
        OrderId orderId,
        CustomerId customerId,
        Address deliveryAddress,
        PaymentStatus paymentStatus,
        OrderStatus orderStatus,
        decimal totalPrice,
        CourierId courierId
                  ) : this(
                      orderId,
                      customerId,
                      deliveryAddress,
                      paymentStatus,
                      orderStatus)
    {
        CourierId = courierId;
    }


    public Order SetCourier(CourierId courierId, CourierStatus courierStatus)
    {
        CheckRule(new TheOrderMustHaveAtLeastOneOrderLine(_orderLines));
        CheckRule(new TheCourierMustHaveValidStatus(courierStatus));
        CheckRule(new PaymentStatusForOrderMustBePaid(PaymentStatus));

        RaiseEvent(new CourierAppointedEvent(courierId));

        return new(OrderId.Create(Id.Value), CustomerId, DeliveryAddress, PaymentStatus, OrderStatus, TotalPrice, courierId);
    }

    public void ChangeOrderStatus(OrderStatus orderStatus)
    {
        CheckRule(new PaymentStatusForOrderMustBePaid(PaymentStatus));
        CheckRule(new OrderStatusChangesMustBeConsistent(OrderStatus, orderStatus));

        this.OrderStatus = orderStatus;

        RaiseEvent(new OrderChangedStatusEvent(orderStatus));
    }

    public void AddOrderLine(Product product, int quantity)
    {
        var newOrderLine = OrderLine.CreateOrderLine(product, quantity);

        _orderLines.Add(newOrderLine);
    }

    public void RemoveOrderLine(OrderLineId orderLineId)
    {
        _orderLines.RemoveWhere(u => u.Id.Value == orderLineId.Value);
    }

    public void UpdateOrderLineQuantity(OrderLineId orderLineId, int quantity)
    {
        var orderLine = _orderLines.Where(u => u.Id.Value == orderLineId.Value).FirstOrDefault();

        if (orderLine is null)
        {
            throw new BusinessException($"Невозможно найти строку заказа с id: {orderLineId.Value}");
        }

        orderLine.ChangeQuantity(quantity);
    }

    decimal GetTotalPrice()
    {
        decimal totalPrice = _orderLines.Select(u => u.Price).Sum();

        return totalPrice > 0 ? totalPrice : 0;
    }

    protected override void Apply(DomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
