using LuckyFoodSystem.AggregationModels.ProductAggregate;
using LuckyFoodSystem.Application.Products.Commands.Create;
using LuckyFoodSystem.Application.Products.Commands.Update;
using LuckyFoodSystem.Application.Products.Common;
using LuckyFoodSystem.Contracts.Product;
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
                  .Map(dest => dest.ShortDescription, src => src.ShortDescription.Value)
                  .Map(dest => dest.Price, src => src.Price.Value)
                  .Map(dest => dest.WeightUnit, src => src.Weight.WeightUnit)
                  .Map(dest => dest.WeightValue, src => src.Weight.WeightValue)
                  .Map(dest => dest.MenusIdS, src => src.Menus.Select(u => u.Id.Value))
                  .Map(dest => dest.Files, src => src.Images.Select(u => u.Path));

            config.NewConfig<ProductResult, ProductResponse>()
                  .Map(dest => dest.Response, src => src.Products.Adapt<List<ProductResponseObject>>());

            config.NewConfig<(CreateProductRequest Request, string rootPath), CreateProductCommand>()
                  .Map(dest => dest.rootPath, src => src.rootPath)
                  .Map(dest => dest, src => src.Request);

            config.NewConfig<(UpdateProductRequest Request, string rootPath, Guid ProductId), UpdateProductCommand>()
                  .Map(dest => dest.rootPath, src => src.rootPath)
                  .Map(dest => dest.ProductId, src => src.ProductId)
                  .Map(dest => dest.DeletingMenusIds, src => src.Request.DeletingMenuIds)
                  .Map(dest => dest.AddingMenusIds, src => src.Request.AddingMenuIds)
                  .Map(dest => dest.ImagesIds, src => src.Request.ImageIds)
                  .Map(dest => dest, src => src.Request);
        }
    }
}
