using AutoMapper;

namespace RestaurantMenu.Utils.Services.Mapper
{
    public class MapperConfigure
    {
        public static IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        }
    }
}
