using LuckyFoodSystem.AggregationModels.ProductAggregate;

namespace LuckyFoodSystem.Application.Products.Common
{
    public record ProductResult(
            List<Product> Products = null!);
}
