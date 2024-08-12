using ErrorOr;
using MediatR;

namespace LuckyFoodSystem.Shared.Mediatr;

public interface IQuery<TResult> : IRequest<ErrorOr<TResult>>
    where TResult : IQueryObject
{
}
