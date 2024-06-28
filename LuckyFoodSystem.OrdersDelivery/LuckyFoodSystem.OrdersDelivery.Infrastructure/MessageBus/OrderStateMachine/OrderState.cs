using LuckyFoodSystem.Orders.Domain.CustomerAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.CourierAggregate;
using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;
using LuckyFoodSystem.OrdersDelivery.Domain.OrderAggregate.Entity.OrderLineEntity;
using LuckyFoodSystem.Shared.Domain.Models.Entity;
using MassTransit;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public OrderStatus CurrentState { get; set; } = null!;

    public bool IsClosed { get; set; }

    public OrderStatus ClosedWithStatus { get; set; } = null!;

    public DateTime OrderStatusChangedAt { get; set; }

    public Address DeliveryAddress { get; private set; } = null!;

    public decimal TotalPrice { get; set; }

    public PaymentStatus PaymentStatus { get; set; } = null!;

    public List<OrderLine> OrderLines { get; set; } = [];

    public CustomerId CustomerId { get; private set; } = null!;

    public CourierId CourierId { get; private set; } = null!;
}
