using ErrorOr;
using LuckyFoodSystem.Application.Menus.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Queries.Read
{
    public record GetAllMenusQuery() : IRequest<ErrorOr<MenuResult>>;
}
