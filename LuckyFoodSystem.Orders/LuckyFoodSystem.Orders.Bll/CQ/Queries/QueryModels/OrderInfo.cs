using LuckyFoodSystem.Orders.Bll.Features.Mediatr;

namespace LuckyFoodSystem.Orders.Bll.CQ.Queries.QueryModels;

public record OrderInfo(
    string OrderStatus,
    string PayMentStatus
    ) : IQueryObject;

