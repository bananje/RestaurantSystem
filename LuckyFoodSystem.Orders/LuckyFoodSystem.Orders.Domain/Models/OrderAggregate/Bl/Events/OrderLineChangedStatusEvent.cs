using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Common;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Bl.Events;

public class OrderLineChangedStatusEvent : DomainEvent, IOrderEvent
{
    public OrderLineChangedStatusEvent(OrderId orderId, OrderLineId orderLineId, ReadyStatus readyStatus)
    {
        AggregateId = orderId.Value;
        OrderLineId = orderLineId;
        ReadyStatus = readyStatus;
    }

    public OrderLineId OrderLineId { get; private set; }

    public ReadyStatus ReadyStatus{ get; private set; }

}
