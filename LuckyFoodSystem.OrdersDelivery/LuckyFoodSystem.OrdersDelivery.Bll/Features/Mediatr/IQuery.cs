using ErrorOr;
using MediatR;

namespace LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;

public interface IQuery<TResult> : IRequest<ErrorOr<TResult>>
    where TResult : IQueryObject
{
}
