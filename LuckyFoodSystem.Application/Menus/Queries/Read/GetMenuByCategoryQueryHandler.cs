using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Queries.Read
{
    public class GetMenuByCategoryQueryHandler
        : IRequestHandler<GetMenuByCategoryQuery, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public GetMenuByCategoryQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }             
        public async Task<ErrorOr<MenuResult>> Handle(GetMenuByCategoryQuery request, CancellationToken cancellationToken)
        {
            var selectedMenu = await _menuRepository.GetMenusByCategoryAsync(request.categoryId);

            if (selectedMenu is null)
            {
                return Errors.Global.CollectionNonExistentException;
            }

            return new MenuResult(selectedMenu);
        }
    }
}
