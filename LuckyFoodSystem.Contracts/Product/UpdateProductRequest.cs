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
            HashSet<Guid> ImageIds = null!,
            HashSet<Guid> DeletingMenuIds = null!,
            HashSet<Guid> AddingMenuIds = null!,
            IFormFileCollection? Files = null);
}
