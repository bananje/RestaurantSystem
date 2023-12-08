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
    internal class UpdateProductCommandHandler
        : IRequestHandler<UpdateMenuCommand, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public UpdateProductCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<MenuResult>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {    
            var menuId = MenuId.Create(request.MenuId);
            var updatedMenu = Menu.Set(menuId,
                                       new Name(request.Name),
                                       Category.FromName(request.Category));   

            Menu? existedMenu = await _menuRepository.GetMenuByIdAsync(menuId, cancellationToken);
            if(existedMenu is null)
            {
                return Errors.Global.ObjectNonExistentException;
            }           
            await _menuRepository.UpdateMenuAsync(menuId, updatedMenu, request.rootPath, cancellationToken, request.ImageIds);

            return new MenuResult(new Menu[] { updatedMenu }.ToList());
        }
    }
}
