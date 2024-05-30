using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    string City,
    string Street,
    string House,
    string ApartmentNum,
    IReadOnlyCollection<OrderLineStruct> OrderLines) 
    : ICommand<CommandResult>;
