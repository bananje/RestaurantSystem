using LuckyFoodSystem.Shared.Contracts.Common.DTO;
using LuckyFoodSystem.Shared.Features;
using LuckyFoodSystem.Shared.Mediatr;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    IReadOnlyCollection<OrderLineStruct> OrderLines) : ICommand<CommandResult>;
