using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Contracts.Products
{
    public record CreateProductRequest(
            string Title,
            string Description,
            string ShortDescription,
            float Price,
            float WeightValue,
            string WeightUnit,
            string Category,
            IFormFileCollection? Files = null);
}
