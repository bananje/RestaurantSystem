using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.CustomerBoundedContext.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string Phone,
    string Address) : ICommand<CommandResult>;

