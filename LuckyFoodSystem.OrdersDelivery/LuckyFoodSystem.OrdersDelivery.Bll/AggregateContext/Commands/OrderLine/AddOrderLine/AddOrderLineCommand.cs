using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Contracts;
using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.OrderLine.AddOrderLine;

/// <summary>
/// Добавить к существующему заказу позицию
/// </summary>
public record AddOrderLineCommand(
    Guid orderId,
    OrderLineRequestStruct OrderLine) : ICommand<CommandResult>;

