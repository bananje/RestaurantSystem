using ErrorOr;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Commands.Update
{
    public record UpdateProductCommand(
            Guid ProductId,
            string Title,
            string Description,
            string ShortDescription,
            float Price,
            float WeightValue,
            string WeightUnit,
            string Category,
            string rootPath,
            List<Guid> MenusIds = null!,
            List<Guid> ImagesIds = null!) : IRequest<ErrorOr<ProductResult>>;
}
