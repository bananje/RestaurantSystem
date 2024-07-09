using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Bl.DomainEvents;
using MassTransit;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.Consumers;

public class OrderConfirmedConsumer : IConsumer<OrderConfirmedEvent>
{
    public Task Consume(ConsumeContext<OrderConfirmedEvent> context)
    {
        var error = new ErrorMessage
    }
}
