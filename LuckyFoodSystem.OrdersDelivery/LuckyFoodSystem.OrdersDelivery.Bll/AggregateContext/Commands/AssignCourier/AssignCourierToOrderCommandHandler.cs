using ErrorOr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using LuckyFoodSystem.OrdersDelivery.Domain.CourierAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.AssignCourier;

public class AssignCourierToOrderCommandHandler : IRequestHandler<AssignCourierToOrderCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    private readonly IEventSourcingRepository<Courier> _courierRepository;

    public AssignCourierToOrderCommandHandler(
        IEventSourcingRepository<Order> orderRepository,
        IEventSourcingRepository<Courier> courierRepository)
    {
        _courierRepository = courierRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CommandResult>> Handle(AssignCourierToOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var courier = await _courierRepository.FindByIdAsync(request.CourierId);

            if (courier is null)
            {
                return Errors.Courier.CourierNotFount(request.CourierId.ToString());
            }

            var order = await _orderRepository.FindByIdAsync(request.OrderId);

            if (order is null)
            {
                return Errors.Order.OrderNotFount(request.CourierId.ToString());
            }

            // назначаем заказу курьера
            order.AssignCourier(courier.Id!, courier.Status);

            await _orderRepository.SaveAsync(order, cancellationToken);

            // назначаем курьеру текущий заказ
            courier.GetOrder(order.Id!, order.CurrentStatus);

            await _courierRepository.SaveAsync(courier);

            return CommandResult.Success(message: $"На заказ ID:{request.OrderId} назначен курьер ID:{request.CourierId}");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
