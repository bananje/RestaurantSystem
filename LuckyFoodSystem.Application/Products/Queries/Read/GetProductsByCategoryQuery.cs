using ErrorOr;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public record GetProductsByCategoryQuery(int categoryId) : IRequest<ErrorOr<ProductResult>>;
}
