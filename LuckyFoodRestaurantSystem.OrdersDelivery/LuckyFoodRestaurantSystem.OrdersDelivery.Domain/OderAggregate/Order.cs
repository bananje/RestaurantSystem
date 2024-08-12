using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CustomerAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Exceptions;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.Rules;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.OrdersDelivery.Domain.Models.CourierAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;
using LuckyFoodSystem.Shared.Domain.Models.Contracts;
using LuckyFoodSystem.Shared.Domain.Models.Entity;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate;

public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<OrderLine> _orderLines = [];

    public OrderStatus CurrentStatus { get; private set; } = OrderStatus.Created;

    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Unpaid;

    public CustomerId CustomerId { get; private set; } = null!;

    public CourierId CourierId { get; private set; } = null!;

    public decimal TotalPrice => GetTotalPrice();

    public decimal CustomerDiscount { get; private set; }

    public Address DeliveryAddress { get; private set; } = null!;

    public bool IsClosed { get; private set; }

    public bool IsCourierAssigned { get; private set; }

    public OrderStatus ClosedWithStatus { get; private set; } = null!;

    public DateTime OrderStatusChangedAt { get; private set; }

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

    public Order() { }

    public Order(
        CustomerId customerId,
        Address deliveryAddress)
    {
        CheckRule(new TheCustomerMustHaveAnAddress(deliveryAddress));

        Id = OrderId.CreateUnique();
        CustomerId = customerId;
        DeliveryAddress = deliveryAddress;
        CurrentStatus = OrderStatus.Created;
        PaymentStatus = PaymentStatus.Unpaid;
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
                      customerId,
                      deliveryAddress)
    {
        CourierId = courierId;
    }

    public void ConfirmOrder()
    {
        CheckRule(new TheOrderMustHaveAtLeastOneOrderLine(_orderLines));

        RaiseEvent(new OrderConfirmedEvent(this));
    }

    public bool CheckСompletion()
    {
        try
        {
            CheckRule(new OrderStatusCanNotBeCompleteWhileDontCompletedAllOrderlines(OrderLines));

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CompleteOrder()
    {
        if (_orderLines.Where(u => u.ReadyStatus == ReadyStatus.Ready).Count() == _orderLines.Count)
        {
            CurrentStatus = OrderStatus.Complete;

            RaiseEvent(new OrderChangedStatusEvent(orderId: this.Id!, OrderStatus.Complete));

            return true;
        }

        return false;
    }

    public Order AssignCourier(CourierId courierId, CourierStatus courierStatus)
    {
        CheckRule(new TheOrderMustHaveAtLeastOneOrderLine(_orderLines));
        CheckRule(new TheCourierMustHaveValidStatus(courierStatus));
        CheckRule(new PaymentStatusForOrderMustBePaid(PaymentStatus));

        if (CourierId is not null)
        {
            throw new CourierAssignedException($"На заказ ID:{Id.Value} уже назначен курьер");
        }

        RaiseEvent(new CourierAppointedEvent(Id, courierId, CurrentStatus));

        return new(OrderId.Create(Id.Value),
            CustomerId,
            DeliveryAddress,
            PaymentStatus,
            CurrentStatus,
            GetTotalPrice(),
            courierId);
    }

    public void CancelOrder()
    {
        CheckRule(new OrderMayBeCanceledOnlyWithStatusAccepted(CurrentStatus));

        if (CurrentStatus == OrderStatus.Canceled)
        {
            throw new OrderCancelledException($"Текущий заказ {Id.Value} уже имеет статус {OrderStatus.Canceled}");
        }

        CloseOrder(OrderStatus.Canceled);
    }

    public void ChangeOrderStatus(OrderStatus orderStatus)
    {
        CheckRule(new PaymentStatusForOrderMustBePaid(PaymentStatus));
        CheckRule(new OrderStatusChangesMustBeConsistent(CurrentStatus, orderStatus));

        if (orderStatus == OrderStatus.Complete)
        {
            CheckRule(new OrderStatusCanNotBeCompleteWhileDontCompletedAllOrderlines(_orderLines));
        }

        CurrentStatus = orderStatus;

        if (orderStatus == OrderStatus.Delivered)
        {
            RaiseEvent(new OrderDeliveredEvent(Id!, CustomerId, CourierId, orderStatus));
        }

        CloseOrder(orderStatus);

        CurrentStatus = orderStatus;

        RaiseEvent(new OrderChangedStatusEvent(Id!, orderStatus));
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
        var newOrderLine = OrderLine.CreateOrderLine(this.Id!, product, quantity);

        _orderLines.Add(newOrderLine);

        CurrentStatus = OrderStatus.Created;

        RaiseEvent(new OrderLineAddedEvent(Id!, newOrderLine));
    }

    public void UpdateOrderLineQuantity(OrderLineId orderLineId, int quantity)
    {
        var orderLine = _orderLines.Where(u => u.Id.Value == orderLineId.Value).FirstOrDefault()
            ?? throw new BusinessException($"Невозможно найти строку заказа с id: {orderLineId.Value}");

        orderLine.ChangeQuantity(quantity);

        RaiseEvent(new OrderLineUpdatedQuantityEvent(Id, orderLineId, quantity));
    }

    public void SetCustomerDiscount(int ordersCount)
    {
        switch (ordersCount)
        {
            case 10: this.CustomerDiscount = 3; break;
            case 20: this.CustomerDiscount = 7; break;
            case 40: this.CustomerDiscount = 10; break;
            case 100: this.CustomerDiscount = 15; break;
        }
    }

    private decimal GetTotalPrice()
    {
        decimal totalPrice = _orderLines.Select(u => u.Price).Sum();

        // применяем пользовательскую скидку
        if (this.CustomerDiscount is not 0)
        {
            totalPrice -= totalPrice * this.CustomerDiscount / 100;
        }

        return totalPrice;
    }

    private void CloseOrder(OrderStatus orderStatus)
    {
        OrderStatusChangedAt = DateTime.UtcNow;
        IsClosed = true;
        CurrentStatus = orderStatus;
        ClosedWithStatus = orderStatus;

        RaiseEvent(new OrderClosedEvent(Id!, CurrentStatus, ClosedWithStatus, IsClosed, OrderStatusChangedAt));
    }


    #region Event Sourcing

    protected override void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case OrderConfirmedEvent @e: OnOrderCreated(@e); break;
            case OrderChangedStatusEvent @e: OnOrderChangedStatus(@e); break;
            case CourierAppointedEvent @e: OnCourierAppointed(e); break;
            case OrderLineAddedEvent @e: OnOrderLineAdded(@e); break;
            case OrderLineRemovedEvent @e: OnOrderLineRemoved(@e); break;
            case OrderLineChangedStatusEvent @e: OnOrderLineChangedStatus(@e); break;
            case OrderLineUpdatedQuantityEvent @e: OnOrderLineUpdatedQuantity(@e); break;
        }
    }

    private void OnOrderCreated(OrderConfirmedEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        CurrentStatus = @event.Order.CurrentStatus;
        PaymentStatus = @event.Order.PaymentStatus;
        DeliveryAddress = @event.Order.DeliveryAddress;
        CustomerId = @event.Order.CustomerId;
    }

    private void OnOrderChangedStatus(OrderChangedStatusEvent @event)
    {
        Id = OrderId.Create(@event.AggregateId);
        CurrentStatus = @event.OrderStatus;
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
