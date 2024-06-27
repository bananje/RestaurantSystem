using ErrorOr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    public CancelOrderCommandHandler(IEventSourcingRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CommandResult>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.FindByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
            {
                return Errors.Order.OrderNotFount(request.OrderId.ToString());
            }

            order.CancelOrder();

            await _orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success(message: $"Заказ ID:{request.OrderId.ToString()} успешно отменён!");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
