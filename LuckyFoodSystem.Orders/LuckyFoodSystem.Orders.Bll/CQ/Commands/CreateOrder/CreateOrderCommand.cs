using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.CQ.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid OrderId,
    Guid CustomerId,
    int OrderStatusCode,
    int PaymentStatusCode,
    string Address) : ICommand<CommandResult>;
