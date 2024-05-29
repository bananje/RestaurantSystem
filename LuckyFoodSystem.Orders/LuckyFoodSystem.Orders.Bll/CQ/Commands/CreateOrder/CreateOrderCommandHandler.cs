using ErrorOr;
using LuckyFoodSystem.Orders.Bll.Results;
using MediatR;

namespace LuckyFoodSystem.Orders.Bll.CQ.Commands.CreateOrder;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, ErrorOr<CommandResult>>
{
    public Task<ErrorOr<CommandResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        
    }
}
