using LuckyFoodSystem.Contracts.Menu;

namespace LuckyFoodSystem.Contracts.Product
{
    public record ProductVM(ProductDTO ProductDTO,
                            List<MenuDTO> Menus,
                            bool IsNewObject);
}
