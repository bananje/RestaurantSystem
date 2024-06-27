using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.Events;
using MapsterMapper;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

public class OrderLineAddedProjection : INotificationHandler<OrderLineAddedEvent>
{
    private readonly IProjectionRepository<OrderInfo> _orderRepository;
    private readonly IMapper _mapper;

    public OrderLineAddedProjection(
        IProjectionRepository<OrderInfo> orderRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderLineAddedEvent notification, CancellationToken cancellationToken)
    {
        OrderInfo order = await _orderRepository.FindAsync(u => u.OrderId == notification.OrderId.Value, cancellationToken);

        if (order is not null)
        {
            order.Version = notification.AggregateVersion;

            var orderLine = _mapper.Map<OrderLineInfo>(notification.OrderLine);

            order.OrderLines.Add(orderLine);

            await _orderRepository.UpdateAsync(order, cancellationToken);
        }
    }
}
