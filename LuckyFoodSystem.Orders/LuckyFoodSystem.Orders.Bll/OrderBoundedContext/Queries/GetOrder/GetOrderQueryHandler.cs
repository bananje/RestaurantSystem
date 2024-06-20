using ErrorOr;
using LuckyFoodSystem.Orders.Bll.OrderBoundedContext.QueryModels;
using LuckyFoodSystem.Orders.Bll.Persistence;
using LuckyFoodSystem.Orders.Bll.Results;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.OrderBoundedContext.Queries.GetOrder;

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
