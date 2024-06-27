namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Contracts;

public record OrderLineRequestStruct(
    Guid ProductId,
    int Quantity);
