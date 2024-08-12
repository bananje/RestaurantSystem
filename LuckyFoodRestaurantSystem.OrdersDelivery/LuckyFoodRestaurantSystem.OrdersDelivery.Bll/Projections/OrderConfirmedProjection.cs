using LuckyFoodRestaurantSystem.Contracts.Shared;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodRestaurantSystem.OrdersDelivery.Contracts;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.Models;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Bl.DomainEvents;
using MapsterMapper;
using MassTransit;
using MediatR;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Projections;

public class OrderConfirmedProjection(
    IMapper mapper,
    IProjectionRepository<OrderInfo> orderRepository,
    IPublishEndpoint publishEndpoint) 
    : INotificationHandler<OrderConfirmedEvent>
{
    public async Task Handle(OrderConfirmedEvent @event, CancellationToken cancellationToken)
    {
        var order = mapper.Map<OrderInfo>(@event.Order);

        await orderRepository.InsertAsync(order);

        IReadOnlyCollection<OrderLineRequestStruct> orderLines =
            order.OrderLines.Select(line => new OrderLineRequestStruct
        {
            ProductId = line.Product.ProductId,
            Quantity = line.Quantity,
        }).ToList();

        await publishEndpoint.Publish<ConfirmOrderArgument>(new
        {
            order.OrderId,
            order.CustomerId,
            orderLines
        });
    }
}
