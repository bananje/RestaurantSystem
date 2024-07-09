using LuckyFoodSystem.OrdersDelivery.Infrastructure.DataAccess.Context.OrderStateMachineDbContext.Models;
using MassTransit;

namespace LuckyFoodSystem.OrdersDelivery.Infrastructure.MessageBus.OrderStateMachine;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public string CurrentState { get; set; } = string.Empty;

    public bool IsClosed { get; set; }

    public string ClosedWithStatus { get; set; } = string.Empty;

    public DateTime OrderStatusChangedAt { get; set; }

    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string House { get; set; } = string.Empty;

    public string ApartmentNum { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; } = string.Empty;

    public IList<OrderLine> OrderLines { get; set; } = [];

    public Guid CustomerId { get; set; }

    public Guid CourierId { get; set; }

    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}