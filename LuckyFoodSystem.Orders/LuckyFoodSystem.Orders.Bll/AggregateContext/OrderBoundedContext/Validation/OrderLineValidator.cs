using FluentValidation;
using LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Contracts;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Validation;

public class OrderLineValidator : AbstractValidator<OrderLineRequestStruct>
{
    public OrderLineValidator()
    {
        RuleFor(u => u.ProductId)
            .NotNull().NotEmpty();

        RuleFor(u => u.Quantity).GreaterThan(0);
    }
}
