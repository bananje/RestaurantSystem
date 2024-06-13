using ErrorOr;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.Results;
using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.ChangeOrderStatus;

public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    public ChangeOrderStatusCommandHandler(IEventSourcingRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CommandResult>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Errors.Order.ProductNotFount(request.OrderId.ToString());
            }

            var orderStatus = OrderStatus.FromName(request.OrderStatus);

            order.ChangeOrderStatus(orderStatus);

            await _orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success($"Статус заказа ID:{order.Id.Value} обновлён до {request.OrderStatus}");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
