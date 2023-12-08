using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Products.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Products.Queries.Read
{
    public record GetProductsByMenuQuery(MenuId MenuId) : IRequest<ErrorOr<ProductResult>>;
}
