using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Queries.GetOrder;

public record GetOrderQuery(Guid OrderId) : IQuery<OrderInfo>;

