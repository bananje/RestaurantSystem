namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Contracts;

public record OrderLineStruct(
    Guid ProductId,
    int Quantity);
