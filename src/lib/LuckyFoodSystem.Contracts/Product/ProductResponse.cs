using LuckyFoodSystem.Contracts.Menu;

namespace LuckyFoodSystem.Contracts.Product
{
    public record ProductResponse(
            List<ProductResponseObject> Response);

    public record ProductResponseObject(
            Guid ProductId,
            string Title,
            string Description,
            string ShortDescription,
            float Price,
            float WeightValue,
            string WeightUnit,
            string Category,
            List<MenuResponseObject> MenusList,
            List<string> Files = null!);  
}
