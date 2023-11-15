using LuckyFoodSystem.AggregationModels.MenuAggregate;
using LuckyFoodSystem.Application.Menus.Commands.Create;
using LuckyFoodSystem.Application.Menus.Common;
using LuckyFoodSystem.Contracts.Menu;
using Mapster;

namespace LuckyFoodSystem.Application.Common.Mapping
{
    public class MenuMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateMenuRequest Request, string rootPath), CreateMenuCommand>()
                  .Map(dest => dest.rootPath, src => src.rootPath)
                  .Map(dest => dest, src => src.Request);

            config.NewConfig<Menu, MenuResponseObject>()
                  .Map(dest => dest.MenuId, src => src.Id.Value)
                  .Map(dest => dest.Name, src => src.Name.Value)
                  .Map(dest => dest.CategoryName, src => src.Category.Name)
                  .Map(dest => dest.Files, src => src.Images.Select(u => u.Path));

            config.NewConfig<MenuResult, MenuResponse>()
                   .Map(dest => dest.Response, src => src.Menus.Adapt<List<MenuResponseObject>>());

        }
    }
}
