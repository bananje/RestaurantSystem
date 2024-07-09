using LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.Events;
using LuckyFoodSystem.OrdersDelivery.Infrastructure.Options;
using LuckyFoodSystem.Shared.Contracts.OrderService.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;

    private readonly IOptions<EndpointsConfiguration> _settings;

    public State Confirmed { get; private set; }

    public State Closed { get; private set; }


    public Event<OrderCreated> OrderCreated { get; private set; }

    public Event<OrderConfirmed> OrderConfirmed { get; private set; }

    public Event<OrderLinesNotConfirmed> OrderLineNotConfirmed { get; private set; }


    public Request<OrderState, IList<GetProductDataRequest>, IList<GetProductDataResponse>> ProductRequest { get; private set; }

    public Request<OrderState, GetDeliveryAddress, GetDeliveryAddressResponse> CustomerRequest { get; private set; }


    public OrderStateMachine(
        ILogger<OrderStateMachine> logger,
        IOptions<EndpointsConfiguration> settings)
    {
        _logger = logger;
        _settings = settings;

        InstanceState(x => x.CurrentState);
        
        BuildStateMachine();

        OnUnhandledEvent(HandleUnhandledEvent);
    }

    private void BuildStateMachine()
    {
        Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderConfirmed, x => x.CorrelateById(context => context.Message.OrderId));
        Event(() => OrderLineNotConfirmed, x => x.CorrelateById(context => context.Message.OrderLines.Select(u => u.OrderLine.OrderId).First()));
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

    private EventActivities<OrderState> WhenOrderLineNotConfirmed()
    {
        return When(OrderLineNotConfirmed)
            .Then(context =>
            {
                context.Saga.CourierId
            })
            .Request(ProductRequest, x => x.Init<GetProductDataRequest>(new
            {
                x.Saga.OrderLines
            }))
            .TransitionTo(ProductRequest.Pending);
    }

    private EventActivities<OrderState> WhenOrderCreated()
    {
        return When(OrderCreated)
            .Then(context =>
            {
                context.Saga.CorrelationId = context.Message.OrderId;
                context.Saga.CustomerId = context.Message.CustomerId;
                context.Saga.OrderLines = context.Message.OrderLines;

                _logger.LogInformation($"[{DateTime.Now}][SAGA] Order submitted. CorrelationId: {context.Instance.CorrelationId}");
            })
            .Request(ProductRequest, x => x.Init<GetProductDataRequest>(new
            {
                x.Saga.OrderLines
            }))
            .Catch<>
            .TransitionTo(ProductRequest.Pending);
    }

    private EventActivities<OrderState> WhenProductDataReturned()
    {
        return When(ProductRequest.Completed)
            .Then(context =>
            {
                context.Saga.OrderLines = context.Message.OrderLines;

                _logger.LogInformation($"[{DateTime.Now}][SAGA] Order submitted. CorrelationId: {context.Saga.CorrelationId}");
            })
            .Request(CustomerRequest, x => x.Init<GetDeliveryAddress>(new { x.Saga.CustomerId }))
            .TransitionTo(CustomerRequest.Pending);
    }

    private EventActivities<OrderState> WhenCustomerDataReturned()
    {
        return When(CustomerRequest.Completed)
            .Then(context =>
            {
                context.Saga.ApartmentNum = context.Message.ApartmentNum;
                context.Saga.City = context.Message.City;
                context.Saga.Street = context.Message.Street;
                context.Saga.House = context.Message.House;

                _logger.LogInformation($"[{DateTime.Now}][SAGA] Order submitted. CorrelationId: {context.Saga.CorrelationId}");
            })
            .TransitionTo(Confirmed);
    }
}
