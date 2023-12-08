using LuckyFoodSystem.Application.Common.Constants;
using LuckyFoodSystem.Application.Common.Interfaces.Clients;
using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Clients;
using LuckyFoodSystem.Infrastructure.Interceptors;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository;
using LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.ProductRepository;
using LuckyFoodSystem.Infrastructure.Services;
using LuckyFoodSystem.Infrastructure.Services.Cache.MemoryCacheService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Library.Clients.IdentityServer;

namespace LuckyFoodSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            AddServices(services);
            AddPersistance(services);
            AddContext(services, configuration);
          
            return services;
        }
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {                      
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.Decorate<IMenuRepository, CachedMenuRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.Decorate<IProductRepository, CachedProductRepository>();           

            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            services.AddDbContext<LuckyFoodDbContext>(options =>
                      options.UseSqlServer(configuration.GetConnectionString(ConnectionNames.ApplicationConnection)));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString(ConnectionNames.Redis);
            });

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextProvider, HttpContextProvider>();
            services.AddTransient<IImageService, ImageService>();
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IIdentityServerClient, IdentityServerClient>();
            services.AddScoped<PublishDomainEventsInterceptor>();

            return services;
        }
    }
}
