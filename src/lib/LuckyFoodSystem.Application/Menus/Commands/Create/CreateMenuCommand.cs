using ErrorOr;
using LuckyFoodSystem.Application.Menus.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Commands.Create
{
    public record CreateMenuCommand(
            string Name,
            string Category,
            string rootPath) : IRequest<ErrorOr<MenuResult>>;
}
