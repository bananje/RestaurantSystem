using FluentValidation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.OrderLine.UpdateOrderLineQuantity;

public class UpdateOrderLineQuantityCommandValidator : AbstractValidator<UpdateOrderLineQuantityCommand>
{
    public UpdateOrderLineQuantityCommandValidator()
    {
        RuleFor(u => u.OrderId).NotEmpty();

        RuleFor(u => u.OrderLineId).NotEmpty();

        RuleFor(u => u.Quantity).GreaterThan(0).NotEmpty();
    }
}
