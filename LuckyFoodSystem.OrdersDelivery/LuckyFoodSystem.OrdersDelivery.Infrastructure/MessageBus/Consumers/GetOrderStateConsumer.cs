using LuckyFoodSystem.Shared.Contracts.OrderService.Contracts;
using MassTransit;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.Consumers;

public class GetOrderStateConsumer : IConsumer<GetOrderStateRequest>
{
    public Task Consume(ConsumeContext<GetOrderStateRequest> context)
    {
        throw new NotImplementedException();
    }
}
