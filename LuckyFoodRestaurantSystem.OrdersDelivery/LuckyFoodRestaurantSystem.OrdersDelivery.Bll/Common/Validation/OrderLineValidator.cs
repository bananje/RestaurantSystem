using FluentValidation;
using LuckyFoodRestaurantSystem.Contracts.Shared;

namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Common.Validation;

public class OrderLineValidator : AbstractValidator<OrderLineRequestStruct>
{
    public OrderLineValidator()
    {
        RuleFor(u => u.ProductId)
            .NotNull().NotEmpty();

        RuleFor(u => u.Quantity)
            .GreaterThanOrEqualTo(0).NotNull();
    }
}
