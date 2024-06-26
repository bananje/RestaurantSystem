using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Commands.AssignCourier;

public record AssignCourierToOrderCommand(
    Guid OrderId,
    Guid CourierId) : ICommand<CommandResult>;
