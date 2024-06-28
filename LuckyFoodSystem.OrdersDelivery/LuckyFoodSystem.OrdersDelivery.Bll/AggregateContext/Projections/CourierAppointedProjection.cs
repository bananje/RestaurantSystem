using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

public class CourierAppointedProjection : INotificationHandler<CourierAppointedEvent>
{
    private readonly IProjectionRepository<OrderInfo> _orderRepository;

    public CourierAppointedProjection(
        IProjectionRepository<OrderInfo> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(CourierAppointedEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;
            order.CurrentStatus = notification.CurrentOrderStatus.Name;
            order.CourierId = notification.CourierId.Value;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
