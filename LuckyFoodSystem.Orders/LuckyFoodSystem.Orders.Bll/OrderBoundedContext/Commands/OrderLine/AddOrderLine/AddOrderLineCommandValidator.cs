using FluentValidation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.AddOrderLine;

public class AddOrderLineCommandValidator : AbstractValidator<AddOrderLineCommand>
{
    public AddOrderLineCommandValidator()
    {
        RuleFor(u => u.OrderLineStruct).NotNull().NotEmpty();
    }
}
