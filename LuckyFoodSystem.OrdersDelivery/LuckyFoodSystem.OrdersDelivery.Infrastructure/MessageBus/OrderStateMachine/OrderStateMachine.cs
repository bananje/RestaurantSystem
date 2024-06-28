using MassTransit;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State Created { get; private set; }


    //public Event<>
}
