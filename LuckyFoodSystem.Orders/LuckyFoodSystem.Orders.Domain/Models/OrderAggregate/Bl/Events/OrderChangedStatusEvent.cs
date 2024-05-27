using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;

public class OrderChangedStatusEvent : DomainEvent, IOrderEvent
{
    public OrderStatus OrderStatus { get; set; }

    public OrderChangedStatusEvent(OrderId orderId, OrderStatus orderStatus)
    {
        AggregateId = orderId.Value;
        OrderStatus = orderStatus;
    }
}
