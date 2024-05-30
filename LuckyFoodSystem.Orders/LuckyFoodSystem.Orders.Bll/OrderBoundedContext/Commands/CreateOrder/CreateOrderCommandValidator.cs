using FluentValidation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(u => u.CustomerId).NotNull().NotEmpty();

        RuleFor(u => u.City).NotEmpty().NotNull();

        RuleFor(u => u.Street).NotNull().NotEmpty();

        RuleFor(u => u.House).NotNull().NotEmpty();

        RuleFor(u => u.ApartmentNum).NotNull().NotEmpty();

        RuleFor(u => u.OrderLines).NotEmpty().NotNull();
    }
}