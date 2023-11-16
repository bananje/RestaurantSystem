﻿using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using LuckyFoodSystem.Infrastructure.Persistаnce.Interceptors;
using LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.MenuRepository;
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
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            AddServices(services);
            AddPersistance(services, configuration);
          

            return services;
        }
        private static IServiceCollection AddPersistance(this IServiceCollection services,
                                                        IConfiguration configuration)
        {
            services.AddDbContext<LuckyFoodDbContext>(options =>
                       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");             
            });
            

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.Decorate<IMenuRepository, CachedMenuRepository>();

            services.AddScoped<PublishDomainEventsInterceptor>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextProvider, HttpContextProvider>();
            services.AddSingleton<IImageService, ImageService>();
            services.AddSingleton<ICacheProvider, CacheProvider>();

            return services;
        }
    }
}
