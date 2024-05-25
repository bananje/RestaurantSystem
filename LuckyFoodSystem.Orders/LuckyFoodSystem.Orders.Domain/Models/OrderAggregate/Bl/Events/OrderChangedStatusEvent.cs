using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;

public class OrderChangedStatusEvent : DomainEvent
{
    public OrderStatus OrderStatus { get; set; }

    public OrderChangedStatusEvent(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}
