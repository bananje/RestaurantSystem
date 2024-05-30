using LuckyFoodSystem.Orders.Bll.Features.Mediatr;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;

public record OrderInfo(
    string OrderStatus,
    string PayMentStatus
    ) : IQueryObject;

