using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Contracts.Product
{
    public record UpdateProductRequest(
            string Title,
            string Description,
            string ShortDescription,
            float Price,
            float WeightValue,
            string WeightUnit,
            string Category,
            List<Guid> ImageIds = null!,
            List<Guid> MenuIds = null!,
            IFormFileCollection? Files = null);
}
