using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.Results;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.CustomerBoundedContext.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string Phone,
    string Address) : ICommand<CommandResult>;

