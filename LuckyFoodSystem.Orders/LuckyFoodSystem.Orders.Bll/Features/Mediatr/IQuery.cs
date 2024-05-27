using ErrorOr;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.Features.Mediatr;

public interface IQuery<TResult> : IRequest<ErrorOr<TResult>>
    where TResult : IQueryObject
{
}
