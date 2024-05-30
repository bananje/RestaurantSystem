using ErrorOr;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.Results;
using LuckyFoodSystem.Orders.Bll.Services;
using LuckyFoodSystem.Orders.Domain.OrderAggregate;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.AddOrderLine;

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
            var order = await _orderRepository.FindByIdAsync(request.orderId);

            if (order is null)
            {
                return Errors.Order.OrderNotFount($"Заказа с id:{request.orderId} не найден");
            }

            var product = await _productService.GetProductByIdAsync(request.OrderLine.ProductId);

            if (product is null)
            {
                return Errors.Order.ProductNotFount($"Продукт с id:{request.OrderLine.ProductId} недоступен к продаже");
            }

            order.AddOrderLine(product, request.OrderLine.Quantity);

            await _orderRepository.SaveAsync(order);

            return CommandResult.Success($"В заказ ID:{order.Id.Value} добавлена новая позиция");
        }
        catch (Exception ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
