using LuckyFoodSystem.Shared.Features;

namespace LuckyFoodSystem.Shared.Contracts.OrderService.Contracts;

public record GetOrderStateRequest
{
    public Guid OrderId { get; init; }
}

public record GetOrderStateResponse
{
    public Guid OrderId { get; init; }

    public string State { get; init; } = string.Empty;

    public IList<StateErrorModel> Errors { get; init; } = [];
}