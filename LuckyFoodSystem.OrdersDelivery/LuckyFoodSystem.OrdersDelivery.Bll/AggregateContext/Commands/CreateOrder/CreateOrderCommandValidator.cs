using FluentValidation;
using LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Validation;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(u => u.CustomerId).NotNull().NotEmpty();

        RuleFor(u => u.City).NotEmpty().NotNull();

        RuleFor(u => u.Street).NotNull().NotEmpty();

        RuleFor(u => u.House).NotNull().NotEmpty();

        RuleFor(u => u.ApartmentNum).NotNull().NotEmpty();

        RuleForEach(u => u.OrderLines).SetValidator(new OrderLineValidator());
    }
}