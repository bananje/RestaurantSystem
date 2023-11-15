﻿using LuckyFoodSystem.Application.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LuckyFoodSystem.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(25);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<ProblemDetailsFactory, LuckyFoodProblemDetailsFactory>();

            return services;
        }
    }
}
