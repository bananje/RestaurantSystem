using FluentValidation;
using LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Common.Validation;


namespace LuckyFoodRestaurantSystem.OrdersDelivery.Bll.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(u => u.CustomerId).NotNull().NotEmpty();

        RuleForEach(u => u.OrderLines).SetValidator(new OrderLineValidator());
    }
}