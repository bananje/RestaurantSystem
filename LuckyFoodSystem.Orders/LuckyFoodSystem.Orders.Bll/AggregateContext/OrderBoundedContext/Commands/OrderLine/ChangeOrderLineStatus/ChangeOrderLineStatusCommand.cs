using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Commands.OrderLine.ChangeOrderLineStatus;

public record ChangeOrderLineStatusCommand(
    Guid OrderId,
    Guid OrderLineId,
    string OrderLineStatus) : ICommand<CommandResult>;
