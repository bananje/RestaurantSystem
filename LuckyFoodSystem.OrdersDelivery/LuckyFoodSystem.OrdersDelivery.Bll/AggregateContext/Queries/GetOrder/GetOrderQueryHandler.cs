using ErrorOr;
using LuckyFoodSystem.OrdersDelivery.Bll.Persistence;
using LuckyFoodSystem.OrdersDelivery.Bll.QueryModels;
using LuckyFoodSystem.OrdersDelivery.Bll.Results;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.AggregateContext.Queries.GetOrder;

public class GetOrderQueryHandler(
    IQueryRepository<OrderInfo> queryRepository
    ) : IRequestHandler<GetOrderQuery, ErrorOr<OrderInfo>>
{
    public async Task<ErrorOr<OrderInfo>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await queryRepository.FindAsync(u => u.OrderId == request.OrderId);

        if (order is null)
        {
            return Errors.Order.OrderNotFount(request.OrderId.ToString());
        }

        return order;
    }
}
