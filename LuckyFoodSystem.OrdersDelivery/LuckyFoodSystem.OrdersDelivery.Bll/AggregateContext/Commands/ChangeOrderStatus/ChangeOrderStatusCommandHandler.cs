using ErrorOr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.ChangeOrderStatus;

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

            return CommandResult.Success(message: $"Статус заказа ID:{order.Id.Value} обновлён до {request.OrderStatus}");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
