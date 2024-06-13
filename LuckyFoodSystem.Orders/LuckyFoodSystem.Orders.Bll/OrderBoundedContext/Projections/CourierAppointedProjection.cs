using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Projections;

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
        var order = await _orderRepository.FindAsync(u => u.Id == notification.OrderId.Value);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;
            order.CurrentStatus = notification.CurrentOrderStatus.Name;
            order.CourierId = notification.CourierId.Value;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
