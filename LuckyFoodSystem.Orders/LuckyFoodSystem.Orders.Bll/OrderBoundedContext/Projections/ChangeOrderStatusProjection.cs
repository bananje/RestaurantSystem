using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Projections;

public class ChangeOrderStatusProjection : INotificationHandler<OrderChangedStatusEvent>
{
    private readonly IProjectionRepository<OrderInfo> _orderRepository;

    public ChangeOrderStatusProjection(
        IProjectionRepository<OrderInfo> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderChangedStatusEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindAsync(u => u.Id == notification.OrderId.Value);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;
            order.CurrentStatus = notification.OrderStatus.Name;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
