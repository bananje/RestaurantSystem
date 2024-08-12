using ErrorOr;
using LuckyFoodRestaurantSystem.Contracts.Shared;
using LuckyFoodSystem.Shared.Features;
using LuckyFoodSystem.Shared.Mediatr;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    IReadOnlyCollection<OrderLineRequestStruct> OrderLines) : ICommand<CommandResult>;