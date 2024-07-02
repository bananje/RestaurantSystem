using LuckyFoodSystem.OrdersDelivery.Infrastructure.Options;
using LuckyFoodSystem.Shared.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    private readonly ILogger<OrderStateMachine> _logger;

    private readonly IOptions<EndpointsConfiguration> _settings;

    public State Created { get; private set; }

    public State AwaitingProductData { get; private set; }

    public State AwaitingCustomerData { get; private set; }

    public State OrderConfirmed { get; private set; }



    public Event<CreateOrder> OrderCreated { get; private set; }

    public Request<OrderState, GetProductData, GetProductDataResponse> ProductRequest { get; private set; }

    public Request<OrderState, GetCustomerData, GetCustomerDataResponse> CustomerRequest { get; private set; }

    public OrderStateMachine(
        ILogger<OrderStateMachine> logger,
        IOptions<EndpointsConfiguration> settings)
    {
        _logger = logger;
        _settings = settings;

        InstanceState(x => x.CurrentState.Name);
        
        BuildStateMachine();

        OnUnhandledEvent(HandleUnhandledEvent);
    }

    private void BuildStateMachine()
    {
        Event(() => OrderCreated, x => x.CorrelateById(context => context.Message.OrderId));
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

    private EventActivities<OrderState> WhenOrderCreated()
    {
        return When(OrderCreated)
            .Then(context =>
            {
                _logger.LogInformation($"[{DateTime.Now}][SAGA] Order submitted. CorrelationId: {context.Instance.CorrelationId}");
            })
            .Request(ProductRequest, x => x.Init<GetProductData>(new { ProductIds = x.Instance.OrderLines.Select(u => u.Id!.Value) }))
            .TransitionTo(ProductRequest.Pending);
    }

    private EventActivities<OrderState> WhenCustomerDataReturned()
    {
        return When(ProductRequest.Completed)
            .Then(context =>
            {
                _logger.LogInformation($"[{DateTime.Now}][SAGA] Order submitted. CorrelationId: {context.Instance.CorrelationId}");
            })
            .Request(CustomerRequest, x => x.Init<GetCustomerData>(new { CustomerId = x.Instance.CustomerId.Value }))
            .TransitionTo(CustomerRequest.Pending);
    }

    private EventActivities<OrderState> WhenOrderConfirmed()
    {
        return When(CustomerRequest.Completed)
            .Then(context =>
            {
                _logger.LogInformation($"[{DateTime.Now}][SAGA] Order submitted. CorrelationId: {context.Instance.CorrelationId}");
            })
            .SendAsync(new Uri(_settings.Value.UserServiceAddress), x => x.Init<>(new
            {
                OrderId = x.Instance.CorrelationId,
                Cart = FromDtoCartPositionToDbConverter.ConvertBackMany(x.Instance.Cart)
            }))
    }
}
