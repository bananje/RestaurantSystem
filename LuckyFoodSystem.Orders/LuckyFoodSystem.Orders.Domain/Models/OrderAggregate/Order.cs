using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate;
using LuckyFoodSystem.Orders.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate;
using LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.Entity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Rules;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.ProductEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate;

public class Order : AggregateRoot<OrderId, IOrderEvent>
{
    private readonly HashSet<OrderLine> _orderLines = [];

    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Accepted;

    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Unpaid;

    public CustomerId CustomerId { get; private set; } = null!;

    public CourierId CourierId { get; private set; } = null!;

    public decimal TotalPrice { get; private set; }

    public Address DeliveryAddress { get; private set; } = null!;

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    public Order() { }

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

        RaiseEvent(new CourierAppointedEvent(Id, courierId));

        return new(OrderId.Create(Id.Value), 
            CustomerId, 
            DeliveryAddress, 
            PaymentStatus,
            OrderStatus, 
            GetTotalPrice(), 
            courierId);
    }

    public void ChangeOrderStatus(OrderStatus orderStatus)
    {
        CheckRule(new PaymentStatusForOrderMustBePaid(PaymentStatus));
        CheckRule(new OrderStatusChangesMustBeConsistent(OrderStatus, orderStatus));

        if (orderStatus == OrderStatus.Complete)
        {
            CheckRule(new OrderStatusCanNotBeCompleteWhileDontCompletedAllOrderlines(_orderLines));
        }

        this.OrderStatus = orderStatus;

        RaiseEvent(new OrderChangedStatusEvent(Id, orderStatus));
    }

    public void ChangeOrderLineStatus(OrderLineId orderLineId, ReadyStatus readyStatus)
    {
        if (!_orderLines.Select(u => u.Id).Contains(orderLineId))
        {
            throw new BusinessException($"Невозможно найти строку заказа с id: {orderLineId.Value}");
        }

        var orderLine = _orderLines.First(u => u.Id.Value == orderLineId.Value);

        orderLine.ChangeStatus(readyStatus);

        RaiseEvent(new OrderLineChangedStatusEvent(Id, orderLineId, readyStatus));
    }

    public void AddOrderLine(Product product, int quantity)
    {
        var newOrderLine = OrderLine.CreateOrderLine(product, quantity);

        _orderLines.Add(newOrderLine);

        TotalPrice = GetTotalPrice();

        RaiseEvent(new OrderLineAddedEvent(Id, newOrderLine));
    }

    public void RemoveOrderLine(OrderLineId orderLineId)
    {
        _orderLines.RemoveWhere(u => u.Id.Value == orderLineId.Value);

        TotalPrice = GetTotalPrice();

        RaiseEvent(new OrderLineRemovedEvent(Id, orderLineId));
    }

    public void UpdateOrderLineQuantity(OrderLineId orderLineId, int quantity)
    {
        var orderLine = _orderLines.Where(u => u.Id.Value == orderLineId.Value).FirstOrDefault()
            ?? throw new BusinessException($"Невозможно найти строку заказа с id: {orderLineId.Value}");

        orderLine.ChangeQuantity(quantity);

        TotalPrice = GetTotalPrice();

        RaiseEvent(new OrderLineUpdatedQuantityEvent(Id, orderLineId, quantity));
    }

    private decimal GetTotalPrice()
    {
        decimal totalPrice = _orderLines.Select(u => u.Price).Sum();

        return totalPrice > 0 ? totalPrice : 0;
    }


    #region Event Sourcing

    protected override void Apply(IOrderEvent @event)
    {
        switch (@event)
        {
            case OrderCreatedEvent @e: OnOrderCreated(@e); break;
            case OrderChangedStatusEvent @e: OnOrderChangedStatus(@e); break;
            case CourierAppointedEvent @e: OnCourierAppointed(e); break;
            case OrderLineAddedEvent @e: OnOrderLineAdded(@e); break;
            case OrderLineRemovedEvent @e: OnOrderLineRemoved(@e); break;
            case OrderLineChangedStatusEvent @e: OnOrderLineChangedStatus(@e); break;
            case OrderLineUpdatedQuantityEvent @e: OnOrderLineUpdatedQuantity(@e); break;
        }
    }

    private void OnOrderCreated(OrderCreatedEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        OrderStatus = @event.OrderStatus;
        PaymentStatus = @event.PaymentStatus;
        DeliveryAddress = @event.DeliveryAddress;
        TotalPrice = @event.TotalPrice;
        CustomerId = @event.CustomerId;
    }

    private void OnOrderChangedStatus(OrderChangedStatusEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        OrderStatus = @event.OrderStatus;
    }

    private void OnCourierAppointed(CourierAppointedEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        CourierId = @event.CourierId;
    }

    private void OnOrderLineAdded(OrderLineAddedEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        _orderLines.Add(@event.OrderLine);
    }

    private void OnOrderLineRemoved(OrderLineRemovedEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        _orderLines.RemoveWhere(u => u.Id.Value == @event.OrderLineId.Value);
    }

    private void OnOrderLineChangedStatus(OrderLineChangedStatusEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);

        var orderLine = _orderLines.First(u => u.Id.Value == @event.OrderLineId.Value);

        orderLine.ChangeStatus(@event.ReadyStatus);
    }

    private void OnOrderLineUpdatedQuantity(OrderLineUpdatedQuantityEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);

        var orderLine = _orderLines.Where(u => u.Id.Value == @event.OrderLineId.Value).FirstOrDefault();

        orderLine!.ChangeQuantity(@event.Quantity);
    }

    #endregion
}
