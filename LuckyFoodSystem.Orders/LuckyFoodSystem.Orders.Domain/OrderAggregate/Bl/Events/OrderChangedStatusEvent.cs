using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderChangedStatusEvent : DomainEvent, IOrderEvent
{
    public OrderStatus OrderStatus { get; set; }

    public OrderChangedStatusEvent(OrderId orderId, OrderStatus orderStatus)
    {
        AggregateId = orderId.Value;
        OrderStatus = orderStatus;
    }
}
