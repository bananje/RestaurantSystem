using FluentValidation;

namespace LuckyFoodSystem.Application.Menus.Commands.Delete
{
    public class DeleteMenuValidator : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuValidator()
        {
            RuleFor(x => x.MenuId.Value).NotNull().NotEmpty();
        }
    }
}
