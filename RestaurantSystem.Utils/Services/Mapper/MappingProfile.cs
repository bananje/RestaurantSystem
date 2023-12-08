using AutoMapper;
using RestaurantMenu.Models.Models;
using RestaurantSystem.Models.Models;

namespace RestaurantMenu.Utils.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Menu, MenuDTO>();
        }
    }
}
