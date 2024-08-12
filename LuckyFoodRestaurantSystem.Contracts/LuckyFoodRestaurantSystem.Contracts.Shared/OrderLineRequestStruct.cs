namespace LuckyFoodRestaurantSystem.Contracts.Shared;

public class OrderLineRequestStruct
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}
