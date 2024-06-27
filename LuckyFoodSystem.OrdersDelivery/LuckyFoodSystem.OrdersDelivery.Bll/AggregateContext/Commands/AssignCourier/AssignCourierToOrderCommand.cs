using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.AssignCourier;

public record AssignCourierToOrderCommand(
    Guid OrderId,
    Guid CourierId) : ICommand<CommandResult>;
