using FluentValidation;

namespace LuckyFoodSystem.Application.Products.Commands.Delete
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId.Value).NotNull().NotEmpty();
        }
    }
}
