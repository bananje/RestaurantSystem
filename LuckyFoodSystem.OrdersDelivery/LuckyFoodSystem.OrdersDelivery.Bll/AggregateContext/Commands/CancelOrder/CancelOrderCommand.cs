using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : ICommand<CommandResult>;
