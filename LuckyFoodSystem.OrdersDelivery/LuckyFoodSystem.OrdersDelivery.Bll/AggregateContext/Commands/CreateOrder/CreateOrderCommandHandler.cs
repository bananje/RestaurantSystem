using ErrorOr;
using LuckyFoodSystem.Orders.Domain.CustomerAggregate;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using LuckyFoodSystem.OrdersDelivery.Bll.Services;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models.Entity;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CreateOrder;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, ErrorOr<CommandResult>>
{
    private readonly IEventSourcingRepository<Order> _orderRepository;

    private readonly IEventSourcingRepository<Customer> _customerRepository;

    private readonly IProductService _productService;

    public CreateOrderCommandHandler(
        IEventSourcingRepository<Order> orderRepository,
        IEventSourcingRepository<Customer> customerRepository,
        IProductService productService)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _productService = productService;
    }

    public async Task<ErrorOr<CommandResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

            if (customer.Id is null)
            {
                return Errors.Customer.CustomerNotFound($"Покупатель с id:{request.CustomerId} не найден");
            }

            var order = new Order(
                customer.Id,
                new Address(
                    request.City,
                    request.Street,
                    request.House,
                    request.ApartmentNum));

            foreach (var item in request.OrderLines)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId, cancellationToken);

                if (product is null)
                {
                    return Errors.Order.ProductNotFount($"Продукт с id:{item.ProductId} недоступен к продаже");
                }

                order.AddOrderLine(product, item.Quantity);
            }

            order.ConfirmOrder();

            await _orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success(message: $"Заказ ID:{order.Id.Value} успешно создан!");
        }
        catch (BusinessException ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
