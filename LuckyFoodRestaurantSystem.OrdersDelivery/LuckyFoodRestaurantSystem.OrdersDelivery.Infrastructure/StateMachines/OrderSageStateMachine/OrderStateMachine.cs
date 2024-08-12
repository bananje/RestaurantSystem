using LuckyFoodRestaurantSystem.OrdersDelivery.Contracts;
using LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.Common.Options;
using LuckyFoodRestaurantSystem.ProductStock.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Infrastructure.StateMachines.OrderSageStateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;

    private readonly IOptions<EndpointsConfiguration> _settings;

    public OrderStateMachine(
        ILogger<OrderStateMachine> logger,
        IOptions<EndpointsConfiguration> settings)
    {
        _logger = logger;
        _settings = settings;

        InstanceState(x => x.CurrentState);

        BuildStateMachine();

        OnUnhandledEvent(HandleUnhandledEvent);

        Initially(WhenOrderCreated());

        During(Confirmed, WhenOrderCreated());

        //DuringAny(WhenOrderRejected());
    }

    public State AwaitConfirmation { get; private set; }

    public State Confirmed { get; private set; }

    public State InProcess { get; private set; }

    public State Complete { get; private set; }

    public State HandedByCourier { get; private set; }

    public State Delivered { get; private set; }

    public State Canceled { get; private set; }

    public State Error { get; private set; }


    public Event<ConfirmOrderArgument> OrderConfirmedEvent { get; private set; }

    public Event<RejectOrderArgument> OrderRejectedEvent { get; private set; }


    private void BuildStateMachine()
    {
        Event(() => OrderConfirmedEvent, x => x.CorrelateById(ctx => ctx.Message.OrderId));
        Event(() => OrderRejectedEvent, x => x.CorrelateById(ctx => ctx.Message.OrderId));

        State(nameof(AwaitConfirmation));
        State(nameof(Confirmed));
    }

    private Task HandleUnhandledEvent(UnhandledEventContext<OrderState> context)
    {
        if (context.Event.Name.Contains("TimeoutExpired"))
        {
            _logger.LogDebug($"[{DateTime.Now}][SAGA] Ignored unhandled event: {context.Event.Name}");

            context.Ignore();
        }
        else
            context.Throw();

        return Task.CompletedTask;
    }

    //private EventActivities<OrderState> WhenOrderRejected()
    //{
    //    return When(OrderRejectedEvent)
    //        .Then(context =>
    //        {
    //            context.Saga.Reason = context.Message.Reason;
    //            context.Saga.CorrelationId = context.Message.OrderId;

    //            _logger.LogInformation($"[{DateTime.Now}] Отмена подтверждения заказа. Заказ ID: {context.Saga.CorrelationId}. Причина: {context.Message.Reason}");
    //        })
    //        .TransitionTo(Rejected)
    //        .Finalize();
    //}

    private EventActivities<OrderState> WhenOrderCreated()
    {
        return When(OrderConfirmedEvent)
            .Then(context =>
            {
                _logger.LogInformation($"[{DateTime.Now}] Уточнение данных по позициям заказа. Заказ ID: {context.Saga.CorrelationId}");
            })
            .TransitionTo(Confirmed)
            .SendAsync(x => x.Init<ProcessOrderArgument>((new
            {
                x.Message.OrderId,
                x.Message.OrderLines
            })))
            .TransitionTo(InProcess);
    }
}
