using ErrorOr;
using MediatR;


namespace LuckyFoodSystem.OrdersDelivery.Bll.Features.Mediatr;

public interface ICommand<TResult> : IRequest<ErrorOr<TResult>>
    where TResult : class 
{
}
