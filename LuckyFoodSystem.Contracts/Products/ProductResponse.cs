namespace LuckyFoodSystem.Contracts.Products
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
            List<Guid> Files = null!);  
}
