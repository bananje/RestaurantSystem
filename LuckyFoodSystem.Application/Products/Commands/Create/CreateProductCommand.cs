using ErrorOr;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Commands.Create
{
    public record CreateProductCommand(
            string Title,
            string Description,
            string ShortDescription,
            float Price,          
            float WeightValue,
            string WeightUnit,
            string Category,
            List<Guid> MenusIds,
            string rootPath) : IRequest<ErrorOr<ProductResult>>;
}
