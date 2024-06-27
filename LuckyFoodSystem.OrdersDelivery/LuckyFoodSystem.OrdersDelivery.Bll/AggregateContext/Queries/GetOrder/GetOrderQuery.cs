using LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Queries.GetOrder;

public record GetOrderQuery(Guid OrderId) : IQuery<OrderInfo>;

