using ErrorOr;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Common.Features;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodRestaurantSystem.OrdersDelivery.Contracts;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CustomerAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.Enumerations;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.OderAggregate.Entity.ProductEntity.ValueObjects;
using LuckyFoodRestaurantSystem.ProductStock.Contracts;
using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Features;
using MassTransit;
using MediatR;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IEventSourcingRepository<Order> orderRepository,
    IEventSourcingRepository<Customer> customerRepository,
    IRequestClient<GetOrderLinesRequest> productStockClient,
    IPublishEndpoint publishEndpoint
) : IRequestHandler<CreateOrderCommand, ErrorOr<CommandResult>>
{
    public async Task<ErrorOr<CommandResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // актуализируем данные юзера в сервисе
            await publishEndpoint.Publish<ActualizeCustomerDataArgument>(new
            {
                request.CustomerId
            });

            // удостоверяемся в наличии товаров в заказе
            var confirmedOrderLines = await productStockClient.GetResponse<GetOrderLinesResponse>(new
            {
                request.OrderLines
            });

            var message = confirmedOrderLines.Message;

            if (!message.IsConfirmed)
            {
                return Errors.Order.ConfirmationOrderLinesError(message.Reason!);
            }

            var customer = await customerRepository.FindByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                return Errors.Customer.NotFound($"Пользователь с Id:{request.CustomerId} не найден!");
            }

            var order = new Order(
                CustomerId.Create(customer.Id!.Value),
                customer!.DeliveryAddress!);

            // добавляем к существующему заказу новые позиции
            foreach (var line in message.OrderLines)
            {
                var responseProduct = line.Product;

                var product = new Product(
                    responseProduct.Title,
                    responseProduct.ProductImageUrl,
                    new ShortDescription(responseProduct.ShortDescription),
                    SaleStatus.Available,
                    new Price(responseProduct.Price),
                    new Discount(responseProduct.Discount),
                    new Weight(25),
                    WeightUnit.FromName("Грамм"));

                order.AddOrderLine(product, line.Quantity);
            }

            order.ConfirmOrder();

            await orderRepository.SaveAsync(order, cancellationToken);

            return CommandResult.Success(message: $"Заказ ID:{order.Id!.Value} успешно создан!");
        }
        catch (BusinessException ex)
        {
            return Errors.Common.BusinessFailure(ex.Message);
        }
    }
}
