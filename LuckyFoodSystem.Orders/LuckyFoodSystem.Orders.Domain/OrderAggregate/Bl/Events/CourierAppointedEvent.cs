using LuckyFoodSystem.Orders.Domain.CourierAggregate;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;


namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class CourierAppointedEvent : DomainEvent
{
    public CourierId CourierId { get; private set; }

    public OrderStatus CurrentOrderStatus { get; private set; }

    public OrderId OrderId { get; private set; }

    public CourierAppointedEvent(OrderId orderId, CourierId courierId, OrderStatus orderStatus)
    {
        AggregateId = orderId.Value;
        OrderId = orderId;
        CourierId = courierId;
        CurrentOrderStatus = orderStatus;
    }
}
