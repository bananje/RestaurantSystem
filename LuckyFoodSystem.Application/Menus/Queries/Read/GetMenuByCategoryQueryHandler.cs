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
            IEnumerable<Menu> selectedMenus = await _menuRepository.GetMenusByCategoryAsync(request.categoryId, cancellationToken);

            if (selectedMenus is null || selectedMenus.Count() is 0)
            {
                return Errors.Global.CollectionNonExistentException;
            }

            return new MenuResult(selectedMenus.ToList());
        }
    }
}
