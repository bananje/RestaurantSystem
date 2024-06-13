using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Projections;

public class OrderLineAddedProjection : INotificationHandler<OrderLineAddedEvent>
{
    private readonly IProjectionRepository<OrderInfo> _orderRepository;

    public OrderLineAddedProjection(IProjectionRepository<OrderInfo> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderLineAddedEvent notification, CancellationToken cancellationToken)
    {
        OrderInfo order = await _orderRepository.FindAsync(u => u.Id == notification.OrderId.Value);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
