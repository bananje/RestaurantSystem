﻿using LuckyFoodSystem.Infrastructure;

namespace LuckyFoodSystem.ProductApi.Grpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGrpcModule(this IServiceCollection services)
        {
            services.AddGrpc();
            services.AddPersistance();

            return services;
        }
    }
}
