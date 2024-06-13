using FluentValidation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(u => u.OrderId).NotNull().NotEmpty();
    }
}
