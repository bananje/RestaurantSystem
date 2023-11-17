using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Commands.Delete
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<MenuResult>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menuId = MenuId.Create(request.MenuId.Value);

            bool result = await _menuRepository.RemoveMenuAsync(menuId, request.rootPath, cancellationToken);

            if (!result)
            {
                return Errors.Global.ObjectNotRemovedException;
            }

            return new MenuResult();
        }
    }
}
