using ErrorOr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using LuckyFoodSystem.OrdersDelivery.Bll.Services;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.OrderLine.AddOrderLine;

public class AddOrderLineCommandHandler : IRequestHandler<AddOrderLineCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    private readonly IProductService _productService;

    public AddOrderLineCommandHandler(
        IEventSourcingRepository<Order> orderRepository,
        IProductService productService)
    {
        _orderRepository = orderRepository;
        _productService = productService;
    }

    public async Task<ErrorOr<CommandResult>> Handle(AddOrderLineCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.FindByIdAsync(request.orderId, cancellationToken);

            if (order is null)
            {
                return Errors.Order.OrderNotFount(request.orderId.ToString());
            }

            var product = await _productService.GetProductByIdAsync(request.OrderLine.ProductId, cancellationToken);

            if (product is null)
            {
                return Errors.Order.ProductNotFount($"Продукт с id:{request.OrderLine.ProductId} недоступен к продаже");
            }

            order.AddOrderLine(product, request.OrderLine.Quantity);

            await _orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success(message: $"В заказ ID:{order.Id.Value} добавлена новая позиция");
        }
        catch (BusinessExceptionBase ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
