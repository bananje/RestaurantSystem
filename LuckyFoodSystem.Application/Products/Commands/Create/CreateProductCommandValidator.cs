using FluentValidation;

namespace LuckyFoodSystem.Application.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private const string NullMessage = "Значение не может быть пустым";
        private const string NegativeValueMessage = "Значение не может быть отрицательным";
        private const string Pattern = @"^[a-zA-Z0-9a-яА-Я\s!@#$%^&*()_+{}\[\]:;<>,.?~\\/`-]+$";

        public CreateProductCommandValidator() 
        {
            RuleFor(c => c.Title).Length(5, 80)
                                 .WithMessage("Допустимая длина наименования от 5 до 80 символов");
            RuleFor(c => c.Title).Matches(Pattern)
                                 .WithMessage("Наименование может состоять только из цифр и букв");
            RuleFor(c => c.Title).NotEmpty().NotNull()
                                 .WithMessage(NullMessage);

            RuleFor(c => c.Description).Length(15, 300)
                                       .WithMessage("Допустимая длина описания от 15 до 300 символов");
            RuleFor(c => c.Description).NotEmpty().NotNull()
                                 .WithMessage(NullMessage);

            RuleFor(c => c.ShortDescription).Length(7, 50)
                                            .WithMessage("Допустимая длина описания от 7 до 50 символов");
            RuleFor(c => c.ShortDescription).NotEmpty().NotNull()
                                            .WithMessage(NullMessage);

            RuleFor(c => c.Price).GreaterThan(0)
                                 .WithMessage(NegativeValueMessage);
            RuleFor(c => c.Price).NotEmpty().NotNull()
                                 .WithMessage(NullMessage);

            RuleFor(c => c.Category).NotEmpty().NotNull()
                                    .WithMessage(NullMessage);

            RuleFor(c => c.WeightValue).GreaterThan(0)
                                       .WithMessage(NegativeValueMessage);
            RuleFor(c => c.WeightValue).NotEmpty().NotNull()
                                       .WithMessage(NullMessage);

            RuleFor(c => c.WeightUnit).NotEmpty().NotNull()
                                      .WithMessage(NullMessage);
        }
    }
}
