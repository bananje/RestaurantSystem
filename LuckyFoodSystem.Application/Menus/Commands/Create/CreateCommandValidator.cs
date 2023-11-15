using FluentValidation;

namespace LuckyFoodSystem.Application.Menus.Commands.Create
{
    public class CreateCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateCommandValidator()
        {
            RuleFor(x => x.Name)
                        .Length(3, 50)
                        .WithMessage("Длина наименования должна от 3 до 50 символов");
            RuleFor(x => x.Name)
                        .NotEmpty()
                        .WithMessage("Значение не может быть пустым");

            RuleFor(x => x.Category).NotNull().NotEmpty();
        }
    }
}
