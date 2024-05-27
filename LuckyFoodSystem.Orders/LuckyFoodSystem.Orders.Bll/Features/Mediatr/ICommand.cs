using ErrorOr;
using MediatR;


namespace LuckyFoodSystem.Orders.Bll.Features.Mediatr;

public interface ICommand<TResult> : IRequest<ErrorOr<TResult>>
    where TResult : class 
{
}
