using FluentValidation;
using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Validation;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.OrderLine.AddOrderLine;

public class AddOrderLineCommandValidator : AbstractValidator<AddOrderLineCommand>
{
    public AddOrderLineCommandValidator()
    {
        RuleFor(c => c.orderId).NotEmpty().NotNull();

        RuleFor(u => u.OrderLine).SetValidator(new OrderLineValidator());
    }
}
