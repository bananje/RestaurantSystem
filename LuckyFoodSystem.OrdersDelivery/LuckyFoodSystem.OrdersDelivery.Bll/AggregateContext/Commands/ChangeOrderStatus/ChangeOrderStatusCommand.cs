using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.ChangeOrderStatus;

public record ChangeOrderStatusCommand(Guid OrderId, string OrderStatus)
    : ICommand<CommandResult>;
