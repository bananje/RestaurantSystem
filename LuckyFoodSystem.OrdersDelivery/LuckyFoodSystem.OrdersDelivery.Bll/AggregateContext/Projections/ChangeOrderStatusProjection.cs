using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

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
        var order = await _orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value, cancellationToken);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;
            order.CurrentStatus = notification.OrderStatus.Name;

            await _orderRepository.UpdateAsync(order, cancellationToken);
        }
    }
}
