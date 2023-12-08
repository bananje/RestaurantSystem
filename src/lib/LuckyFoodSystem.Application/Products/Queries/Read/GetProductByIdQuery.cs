using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public record GetProductByIdQuery(ProductId ProductId) : IRequest<ErrorOr<ProductResult>>;
}
