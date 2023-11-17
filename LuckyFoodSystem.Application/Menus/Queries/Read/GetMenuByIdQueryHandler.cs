using ErrorOr;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Domain.AggregationModels.Errors;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Queries.Read
{
    public class GetMenuByIdQueryHandler
        : IRequestHandler<GetMenuByIdQuery, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }      
        public async Task<ErrorOr<MenuResult>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var selectedMenu = await _menuRepository.GetMenuByIdAsync(request.MenuId);

            if(selectedMenu is null)
            {
               return Errors.Global.ObjectNonExistentException;
            }

            List<Menu> menus = new() { selectedMenu };
            return new MenuResult(menus);
        }
    }
}
