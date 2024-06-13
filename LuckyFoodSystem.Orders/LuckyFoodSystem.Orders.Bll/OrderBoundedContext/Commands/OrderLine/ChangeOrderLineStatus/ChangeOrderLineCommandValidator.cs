using FluentValidation;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Validation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.ChangeOrderLineStatus;

public class ChangeOrderLineCommandValidator : AbstractValidator<ChangeOrderLineStatusCommand>
{
    public ChangeOrderLineCommandValidator()
    {
        RuleFor(u => u.OrderLineStatus)
            .NotNull()
            .NotEmpty()
            .MustAsync(Rules.IsOrderLineStatusValid);

        RuleFor(u => u.OrderId)
            .NotEmpty()
            .NotNull();

        RuleFor(u => u.OrderLineId)
            .NotEmpty()
            .NotNull();
    }
}
