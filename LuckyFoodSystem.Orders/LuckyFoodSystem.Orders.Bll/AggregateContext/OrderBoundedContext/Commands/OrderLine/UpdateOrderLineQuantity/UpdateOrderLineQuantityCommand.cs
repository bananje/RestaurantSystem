using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Commands.OrderLine.UpdateOrderLineQuantity;

public record UpdateOrderLineQuantityCommand(Guid OrderId, Guid OrderLineId, int Quantity)
    : ICommand<CommandResult>;
