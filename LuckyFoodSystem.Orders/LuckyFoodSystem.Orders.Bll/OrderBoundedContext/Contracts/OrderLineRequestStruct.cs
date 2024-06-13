namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;

public record OrderLineRequestStruct(
    Guid ProductId,
    int Quantity);
