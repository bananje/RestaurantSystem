using LuckyFoodSystem.OrdersDelivery.Domain.Models.OrderAggregate.Enumerations;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Validation;

public static class Rules
{
    public static async Task<bool> IsOrderLineStatusValid(string status, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return OrderStatus.FromName(status) is not null;
    }
}
