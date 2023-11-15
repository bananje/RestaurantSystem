using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using LuckyFoodSystem.Infrastructure.Persistаnce.Interceptors;
using LuckyFoodSystem.Infrastructure.Persistаnce.Repositories;
using LuckyFoodSystem.Infrastructure.Services;
using LuckyFoodSystem.Infrastructure.Services.MemoryCacheService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyFoodSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            AddServices(services);
            AddPersistance(services, configuration);

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            return services;
        }
        private static IServiceCollection AddPersistance(this IServiceCollection services,
                                                        IConfiguration configuration)
        {
            services.AddDbContext<LuckyFoodDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<PublishDomainEventsInterceptor>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();
            services.AddSingleton<IHttpContextProvider, HttpContextProvider>();
            services.AddSingleton<IImageService, ImageService>();

            return services;
        }
    }
}
