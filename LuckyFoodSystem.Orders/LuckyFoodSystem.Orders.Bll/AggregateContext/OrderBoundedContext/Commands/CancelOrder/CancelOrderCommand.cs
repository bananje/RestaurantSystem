using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Commands.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : ICommand<CommandResult>;
