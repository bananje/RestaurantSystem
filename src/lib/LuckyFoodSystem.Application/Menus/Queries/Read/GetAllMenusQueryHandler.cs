using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Queries.Read
{
    public class GetAllMenusQueryHandler
        : IRequestHandler<GetAllMenusQuery, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public GetAllMenusQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<MenuResult>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Menu> menus = await _menuRepository.GetMenusAsync(cancellationToken);
            if (menus.Count() is 0)
            {
                return Errors.Global.CollectionNonExistentException;
            }

            return new MenuResult(menus.ToList());
        }
    }
}
