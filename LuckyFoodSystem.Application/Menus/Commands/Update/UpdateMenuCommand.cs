using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Menus.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Commands.Update
{
    public record UpdateMenuCommand(
        Guid MenuId,
        string Name,
        string Category,
        List<Guid> ImageIds,
        string rootPath) : IRequest<ErrorOr<MenuResult>>;
}
