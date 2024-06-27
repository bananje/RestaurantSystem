using FluentValidation;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(u => u.OrderId).NotNull().NotEmpty();
    }
}
