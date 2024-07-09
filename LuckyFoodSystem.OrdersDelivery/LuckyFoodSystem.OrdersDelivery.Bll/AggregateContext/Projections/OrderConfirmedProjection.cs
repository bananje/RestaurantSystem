using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;
using LuckyFoodSystem.Shared.Models;
using MapsterMapper;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Projections;

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
