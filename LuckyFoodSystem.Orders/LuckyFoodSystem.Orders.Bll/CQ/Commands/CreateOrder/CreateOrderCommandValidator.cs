using FluentValidation;

namespace LuckyFoodSystem.Orders.Bll.CQ.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(u => u.Address).NotNull().NotEmpty();

        RuleFor(u => u.OrderId).NotEmpty().NotNull();

        RuleFor(u => u.CustomerId).NotNull().NotEmpty();

        RuleFor(u => u.OrderStatusCode).GreaterThan(0).NotNull();

        RuleFor(u => u.PaymentStatusCode).GreaterThan(0).NotNull();
    }
}