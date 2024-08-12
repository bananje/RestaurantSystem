using ErrorOr;
using MediatR;


namespace LuckyFoodSystem.Shared.Mediatr;

public interface ICommand<TResult> : IRequest<ErrorOr<TResult>>
    where TResult : class 
{
}
