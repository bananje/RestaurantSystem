using FluentValidation;
using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Validation;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.ChangeOrderStatus;

public class ChangeOrderStatusValidator : AbstractValidator<ChangeOrderStatusCommand>
{
    public ChangeOrderStatusValidator()
    {
        RuleFor(u => u.OrderId).NotEmpty();

        RuleFor(u => u.OrderStatus).MustAsync(Rules.IsOrderLineStatusValid);
    }
}
