using LuckyFoodSystem.AggregationModels.Common.Enumerations;
using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.AggregationModels.MenuAggregate.ValueObjects;
using ErrorOr;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Menus.Common;
using MediatR;

namespace LuckyFoodSystem.Application.Menus.Commands.Create
{
    public class CreateMenuCommandHandler
                        : IRequestHandler<CreateMenuCommand, ErrorOr<MenuResult>>
    {
        private readonly IMenuRepository _menuRepository;
        public CreateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ErrorOr<MenuResult>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = Menu.Create(new Name(request.Name),
                                   Category.FromName(request.Category));

            await _menuRepository.AddMenuAsync(menu, request.rootPath);

            return new MenuResult(new Menu[] { menu }.ToList());
        }
    }
}
