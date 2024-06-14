using ErrorOr;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.Results;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Entity.OrderLineEntity.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Orders.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.ChangeOrderLineStatus;

public class ChangeOrderLineStatusCommandHandler : IRequestHandler<ChangeOrderLineStatusCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    public ChangeOrderLineStatusCommandHandler(IEventSourcingRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CommandResult>> Handle(ChangeOrderLineStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Errors.Order.OrderNotFount(request.OrderId.ToString());
            }

            var status = ReadyStatus.FromName(request.OrderLineStatus);

            order.ChangeOrderLineStatus(
                OrderLineId.Create(request.OrderLineId),
                status);

            // Если это последняя незавершенная строка заказа
            if (order.CheckСompletion())
            {
                order.CompleteOrder();
            }

            await _orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success(message:$"Статус позиции заказа ID:{request.OrderLineId} обновлен на {status.Name}");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
