using ErrorOr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.OrderLine.UpdateOrderLineQuantity;

public class UpdateOrderLineQuantityCommandHandler : IRequestHandler<UpdateOrderLineQuantityCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    public UpdateOrderLineQuantityCommandHandler(IEventSourcingRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CommandResult>> Handle(UpdateOrderLineQuantityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Errors.Order.OrderNotFount(request.OrderId.ToString());
            }

            order.UpdateOrderLineQuantity(
                OrderLineId.Create(request.OrderLineId),
                request.Quantity);

            await _orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success(message: $"У позиции заказа ID:{order.Id.Value} обновлено количество продуктов на {request.Quantity}");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
