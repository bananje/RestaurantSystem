using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.ChangeOrderStatus;

public record ChangeOrderStatusCommand(Guid OrderId, string OrderStatus) 
    : ICommand<CommandResult>;
