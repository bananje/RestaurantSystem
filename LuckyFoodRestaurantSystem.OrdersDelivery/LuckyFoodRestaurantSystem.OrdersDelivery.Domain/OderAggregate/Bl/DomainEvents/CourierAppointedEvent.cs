using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

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
