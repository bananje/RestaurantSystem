using FluentValidation;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Validation;

public class OrderLineValidator : AbstractValidator<OrderLineRequestStruct>
{
    public OrderLineValidator()
    {
        RuleFor(u => u.ProductId)
            .NotNull().NotEmpty();

        RuleFor(u => u.Quantity).GreaterThan(0);
    }
}
