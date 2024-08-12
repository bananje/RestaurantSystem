using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodRestaurantSystem.OrdersDelivery.Contracts;
using LuckyFoodRestaurantSystem.OrdersDelivery.Domain.CustomerAggregate;
using LuckyFoodRestaurantSystem.Shared.Features;
using LuckyFoodRestaurantSystem.Users.Contracts;
using LuckyFoodSystem.Shared.Domain.Models.Entity;
using LuckyFoodSystem.Shared.Domain.Models.ValueObjects;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Consumers;

public class CustomerActualizingDataConsumer(
    IEventSourcingRepository<Customer> customerRepository,
    IRequestClient<ActualizeCustomerInfoRequest> customerClient,
    ILogger<CustomerActualizingDataConsumer> logger) : IConsumer<ActualizeCustomerDataArgument>
{
    public async Task Consume(ConsumeContext<ActualizeCustomerDataArgument> context)
    {
        var response = await customerClient.GetResponse<ActualizeCustomerInfoResponse>(new
        {
            context.Message.CustomerId
        });

        var message = response.Message;

        if (!message.IsCompleted)
        {
            await Task.CompletedTask;

            string reason = Strings.ServiceUnavailable(ServiceNames.ProductService, message.Reason ?? null);

            logger.LogError(reason);

            return;
        }

        // ищем юзера в локальном хранилище сервиса
        var customer = await customerRepository.FindByIdAsync(message.CustomerId, context.CancellationToken);     

        // обновление данных юзера в локальном хранилище сервиса
        if (customer is not null)
        {
            // обновляем данные юзера
            customer.UpdateCustomerInfo(
                new CustomerId(context.Message.CustomerId),
                message.FirstName,
                message.MiddleName,
                message.LastName,
                new Email(message.Email),
                new Phone(message.Phone),
                new Address(
                    message.City,
                    message.Street,
                    message.House,
                    message.ApartmentNum));
        }
        else
        {
            // создаём юзера в хранилище
            customer = new Customer(
                    message.FirstName,
                    message.MiddleName,
                    message.LastName,
                    new Email(message.Email),
                    new Phone(message.Phone),
                    new Address(
                        message.City,
                        message.Street,
                        message.House,
                        message.ApartmentNum));
        }

        await customerRepository.SaveAsync(customer, context.CancellationToken);

        await Task.CompletedTask;
    }
}
