using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.OrderLine.ChangeOrderLineStatus;

public record ChangeOrderLineStatusCommand(
    Guid OrderId,
    Guid OrderLineId,
    string OrderLineStatus) : ICommand<CommandResult>;
