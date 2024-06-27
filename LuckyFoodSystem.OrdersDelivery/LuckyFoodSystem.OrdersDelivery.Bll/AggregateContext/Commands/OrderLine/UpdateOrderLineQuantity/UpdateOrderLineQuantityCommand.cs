using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.OrderLine.UpdateOrderLineQuantity;

public record UpdateOrderLineQuantityCommand(Guid OrderId, Guid OrderLineId, int Quantity)
    : ICommand<CommandResult>;
