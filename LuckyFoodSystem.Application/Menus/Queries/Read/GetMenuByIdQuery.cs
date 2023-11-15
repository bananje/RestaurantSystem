using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Menus.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Queries.Read
{
    public record GetMenuByIdQuery(MenuId MenuId) : IRequest<ErrorOr<MenuResult>>;
}
