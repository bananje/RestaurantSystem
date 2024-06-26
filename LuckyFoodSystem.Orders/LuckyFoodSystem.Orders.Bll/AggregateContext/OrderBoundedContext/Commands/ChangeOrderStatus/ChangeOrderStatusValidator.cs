using FluentValidation;
using LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Validation;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Commands.ChangeOrderStatus;

public class ChangeOrderStatusValidator : AbstractValidator<ChangeOrderStatusCommand>
{
    public ChangeOrderStatusValidator()
    {
        RuleFor(u => u.OrderId).NotEmpty();

        RuleFor(u => u.OrderStatus).MustAsync(Rules.IsOrderLineStatusValid);
    }
}
