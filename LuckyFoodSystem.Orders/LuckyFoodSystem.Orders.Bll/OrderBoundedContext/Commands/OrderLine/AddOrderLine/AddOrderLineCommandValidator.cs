using FluentValidation;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Validation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.AddOrderLine;

public class AddOrderLineCommandValidator : AbstractValidator<AddOrderLineCommand>
{
    public AddOrderLineCommandValidator()
    {
        RuleFor(c => c.orderId).NotEmpty().NotNull();

        RuleFor(u => u.OrderLine).SetValidator(new OrderLineValidator());
    }
}
