using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Shared.Domain.Models;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;

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
