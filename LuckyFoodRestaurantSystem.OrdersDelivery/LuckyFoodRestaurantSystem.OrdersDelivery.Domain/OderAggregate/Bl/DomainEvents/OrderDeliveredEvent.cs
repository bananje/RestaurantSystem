using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CourierAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CustomerAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

public class OrderDeliveredEvent : DomainEvent
{
    public OrderStatus OrderStatus { get; private set; }

    public OrderId OrderId { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public CourierId CourierId { get; private set; }

    public OrderDeliveredEvent(
        OrderId orderId,
        CustomerId customerId,
        CourierId courseId,
        OrderStatus currentStatus)
    {
        AggregateId = orderId.Value;

        OrderId = orderId;
        CustomerId = customerId;
        CourierId = courseId;
        OrderStatus = currentStatus;
    }
}
