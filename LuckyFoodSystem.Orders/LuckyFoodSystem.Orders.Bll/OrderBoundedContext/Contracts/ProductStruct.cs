namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;

public record ProductStruct(
    Guid ProductId,
    string Title,
    string Description,
    string ShortDescription,
    string ProductImageUrl,
    decimal Price,
    decimal? BaseDiscount,
    decimal? MaxDiscount,
    float WeightValue,
    int weightUnitStatusCode,
    int saleStatusCode);
