using LuckyFoodSystem.Contracts.Menu;

namespace LuckyFoodSystem.Contracts.Product
{
    public record ProductDTO(Guid ProductId,
                             string Title,
                             string Description,
                             string ShortDescription,
                             float Price,
                             float WeightValue,
                             string WeightUnit,
                             string Category,
                             List<MenuDTO> Menus,
                             List<string> Images);
}
