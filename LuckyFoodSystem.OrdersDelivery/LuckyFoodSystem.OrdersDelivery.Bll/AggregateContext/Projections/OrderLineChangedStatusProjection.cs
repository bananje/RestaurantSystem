using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

public class OrderLineChangedStatusProjection(
    IProjectionRepository<OrderInfo> orderRepository)
    : INotificationHandler<OrderLineChangedStatusEvent>
{
    public async Task Handle(OrderLineChangedStatusEvent notification, CancellationToken cancellationToken)
    {
        OrderInfo order = await orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value);

        if (order is not null)
        {
            var orderLine = order.OrderLines.FirstOrDefault(u => u.OrderLineId == notification.OrderLineId.Value);

            if (orderLine is not null)
            {
                orderLine.ReadyStatus = notification.ReadyStatus.Name;
            }

            order.Version = notification.AggregateVersion;

            await orderRepository.UpdateAsync(order, cancellationToken);
        }
    }
}
