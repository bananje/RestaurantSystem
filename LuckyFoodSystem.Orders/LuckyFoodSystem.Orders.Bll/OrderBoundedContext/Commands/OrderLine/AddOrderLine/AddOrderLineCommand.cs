using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.AddOrderLine;

/// <summary>
/// Добавить к существующему заказу позицию
/// </summary>
public record AddOrderLineCommand(
    Guid orderId,
    OrderLineRequestStruct OrderLine) : ICommand<CommandResult>;

