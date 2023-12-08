using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Contracts.Menu
{
    public record UpdateMenuRequest(
            string? Name = null,
            string? Category = null,
            IFormFileCollection? Files = null,
            HashSet<Guid>? ImageIds = null);
}
