using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Contracts;
using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    string City,
    string Street,
    string House,
    string ApartmentNum,
    IReadOnlyCollection<OrderLineRequestStruct> OrderLines)
    : ICommand<CommandResult>;
