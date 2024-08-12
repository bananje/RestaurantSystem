using MassTransit;

namespace LuckyFoodRestaurantSystem.Shared.Domain.Saga;

public class SagaStateBase : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }

    public string CurrentState { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public byte[] RowVersion { get; set; } = [];
}
