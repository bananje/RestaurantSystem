using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;

public class OrderLineChangedStatusEvent : DomainEvent
{
    public OrderLineChangedStatusEvent(OrderId orderId, OrderLineId orderLineId, ReadyStatus readyStatus)
    {
        AggregateId = orderId.Value;
        OrderLineId = orderLineId;
        ReadyStatus = readyStatus;
    }

    public OrderLineId OrderLineId { get; private set; }

    public ReadyStatus ReadyStatus { get; private set; }

}
