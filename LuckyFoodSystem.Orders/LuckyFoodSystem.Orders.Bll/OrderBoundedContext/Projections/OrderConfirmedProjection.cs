using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Bl.Events;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Projections;

public class OrderConfirmedProjection(
    IProjectionRepository<OrderInfo> repository) : INotificationHandler<OrderConfirmedEvent>
{
    public async Task Handle(OrderConfirmedEvent @event, CancellationToken cancellationToken)
    {
        OrderInfo order = new();

        

        await repository.InsertAsync(@event.Order);
    }
}
