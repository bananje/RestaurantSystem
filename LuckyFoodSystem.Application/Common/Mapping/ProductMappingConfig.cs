using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Contracts.Menu;
using LuckyFoodSystem.Contracts.Products;
using Mapster;

namespace LuckyFoodSystem.Application.Common.Mapping
{
    public class ProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductResponseObject>()
                 .Map(dest => dest.ProductId, src => src.Id.Value)
                 .Map(dest => dest.Title, src => src.Title.Value)
                 .Map(dest => dest.Category, src => src.Category.Name)
                 .Map(dest => dest.Description, src => src.Description.Value)
                 .Map(dest => dest.Price, src => src.Price.Value)
                 .Map(dest => dest.WeightUnit, src => src.Weight.WeightUnit)
                 .Map(dest => dest.WeightValue, src => src.Weight.WeightValue)
                 .Map(dest => dest.Files, src => src.Images.Select(u => u.Path));

            config.NewConfig<ProductResult, ProductResponse>()
                   .Map(dest => dest.Response, src => src.Products.Adapt<List<MenuResponseObject>>());
        }
    }
}
