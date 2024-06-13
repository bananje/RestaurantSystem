using ErrorOr;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.ChangeOrderLineStatus;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.Results;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.UpdateOrderLineQuantity;

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

            return CommandResult.Success($"У позиции заказа ID:{order.Id.Value} обновлено количество продуктов на {request.Quantity}");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
