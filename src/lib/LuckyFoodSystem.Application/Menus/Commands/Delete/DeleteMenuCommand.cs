using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Menus.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Commands.Delete
{
    public record DeleteMenuCommand(MenuId MenuId,
                                    string rootPath) : IRequest<ErrorOr<MenuResult>>;

}
