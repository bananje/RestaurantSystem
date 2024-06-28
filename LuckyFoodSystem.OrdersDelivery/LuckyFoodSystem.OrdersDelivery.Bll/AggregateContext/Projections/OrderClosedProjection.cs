using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

public class OrderClosedProjection(
    IProjectionRepository<OrderInfo> orderRepository)
    : INotificationHandler<OrderClosedEvent>
{
    public async Task Handle(OrderClosedEvent notification, CancellationToken cancellationToken)
    {
        OrderInfo order = await orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value, cancellationToken);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;
            order.CurrentStatus = notification.CurrentStatus.Name;
            order.ClosedWithStatus = notification.ClosedWithStatus.Name;
            order.IsClosed = notification.IsClosed;
            order.OrderStatusChangedAt = notification.OrderStatusChangedAt;

            await orderRepository.UpdateAsync(order, cancellationToken);
        }
    }
}
