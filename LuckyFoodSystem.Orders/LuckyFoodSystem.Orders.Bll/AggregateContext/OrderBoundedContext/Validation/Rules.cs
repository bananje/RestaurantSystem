using LuckyFoodSystem.Orders.Domain.Models.OrderAggregate.Enumerations;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Validation;

public static class Rules
{
    public static async Task<bool> IsOrderLineStatusValid(string status, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return OrderStatus.FromName(status) is not null;
    }
}
