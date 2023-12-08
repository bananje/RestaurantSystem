using ErrorOr;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Commands.Delete
{
    public record DeleteProductCommand(ProductId ProductId,
                                       string rootPath) : IRequest<ErrorOr<ProductResult>>;

}
