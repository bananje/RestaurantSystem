using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;
using MapsterMapper;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Projections;

public class OrderConfirmedProjection(
    IProjectionRepository<OrderInfo> repository,
    IMapper mapper) : INotificationHandler<OrderConfirmedEvent>
{
    public async Task Handle(OrderConfirmedEvent @event, CancellationToken cancellationToken)
    {
        var order = mapper.Map<OrderInfo>(@event.Order);

        await repository.InsertAsync(order);
    }
}
