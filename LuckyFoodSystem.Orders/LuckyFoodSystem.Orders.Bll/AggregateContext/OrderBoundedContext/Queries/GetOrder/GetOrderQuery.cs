using LuckyFoodSystem.Orders.Bll.Features.Mediatr;
using LuckyFoodSystem.Orders.Bll.QueryModels;

namespace LuckyFoodSystem.Orders.Bll.AggregateContext.OrderBoundedContext.Queries.GetOrder;

public record GetOrderQuery(Guid OrderId) : IQuery<OrderInfo>;

