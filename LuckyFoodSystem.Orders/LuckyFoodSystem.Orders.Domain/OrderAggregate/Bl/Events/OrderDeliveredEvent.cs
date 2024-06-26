using LuckyFoodSystem.Orders.Domain.CourierAggregate;
using LuckyFoodSystem.Orders.Domain.CustomerAggregate;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

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
