using LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Contracts;
using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    string City,
    string Street,
    string House,
    string ApartmentNum,
    IReadOnlyCollection<OrderLineRequestStruct> OrderLines)
    : ICommand<CommandResult>;
