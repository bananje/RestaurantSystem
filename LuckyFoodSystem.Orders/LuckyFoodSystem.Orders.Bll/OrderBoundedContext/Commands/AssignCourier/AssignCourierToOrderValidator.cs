using FluentValidation;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Commands.AssignCourier;

public class AssignCourierToOrderValidator : AbstractValidator<AssignCourierToOrderCommand>
{
    public AssignCourierToOrderValidator()
    {
        RuleFor(u => u.OrderId).NotEmpty().NotNull();

        RuleFor(u => u.CourierId).NotEmpty().NotNull();
    }
}
