using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;

public class OrderLineChangedStatusEvent : DomainEvent
{
    public OrderLineChangedStatusEvent(OrderId orderId, OrderLineId orderLineId, ReadyStatus readyStatus)
    {
        AggregateId = orderId.Value;
        OrderId = orderId;
        OrderLineId = orderLineId;
        ReadyStatus = readyStatus;
    }

    public OrderId OrderId { get; private set; }

    public OrderLineId OrderLineId { get; private set; }

    public ReadyStatus ReadyStatus { get; private set; }

}
