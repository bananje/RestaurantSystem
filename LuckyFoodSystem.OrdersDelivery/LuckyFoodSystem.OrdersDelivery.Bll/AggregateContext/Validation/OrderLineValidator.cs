using FluentValidation;
using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Contracts;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Validation;

public class OrderLineValidator : AbstractValidator<OrderLineRequestStruct>
{
    public OrderLineValidator()
    {
        RuleFor(u => u.ProductId)
            .NotNull().NotEmpty();

        RuleFor(u => u.Quantity).GreaterThan(0);
    }
}
