using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Contracts.Menu
{
    public record CreateMenuRequest(           
            string Name,
            string Category,
            IFormFileCollection Files);
}
