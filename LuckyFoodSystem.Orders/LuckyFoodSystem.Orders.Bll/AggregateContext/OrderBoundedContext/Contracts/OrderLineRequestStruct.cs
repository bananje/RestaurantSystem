namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Contracts;

public record OrderLineRequestStruct(
    Guid ProductId,
    int Quantity);
