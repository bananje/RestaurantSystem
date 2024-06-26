using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.QueryModels;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Projections;

public class OrderLineUpdatedQuantityProjection(
    IProjectionRepository<OrderInfo> orderRepository)
    : INotificationHandler<OrderLineUpdatedQuantityEvent>
{
    public async Task Handle(OrderLineUpdatedQuantityEvent notification, CancellationToken cancellationToken)
    {
        OrderInfo order = await orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value, cancellationToken);

        if (order is not null)
        {
            var orderLine = order.OrderLines.FirstOrDefault(u => u.OrderLineId == notification.OrderLineId.Value);

            if (orderLine is not null)
            {
                orderLine.Quantity = notification.Quantity;
            }

            order.Version = notification.AggregateVersion;

            await orderRepository.UpdateAsync(order, cancellationToken);
        }
    }
}
