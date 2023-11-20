using ErrorOr;
using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;


namespace LuckyFoodSystem.Application.Menus.Commands.Update
{
    internal class UpdateMenuCommandHandler
        : IRequestHandler<UpdateMenuCommand, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public UpdateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<MenuResult>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {    
            var menuId = MenuId.Create(request.MenuId);
            var updatedMenu = Menu.Set(menuId,
                                       new Name(request.Name),
                                       Category.FromName(request.Category));   

            Menu? existedMenu = await _menuRepository.GetMenuByIdAsync(menuId);
            if(existedMenu is null)
            {
                return Errors.Global.ObjectNonExistentException;
            }

            existedMenu = Menu.UpdateMenu(existedMenu, updatedMenu);

            await _menuRepository.UpdateMenuAsync(existedMenu, request.rootPath, cancellationToken, request.ImageIds);

            Menu[] menus = { updatedMenu };
            return new MenuResult(menus.ToList());
        }
    }
}
