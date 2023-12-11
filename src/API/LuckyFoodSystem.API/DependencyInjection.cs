using LuckyFoodSystem.Application.Common.Errors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OnlineShop.Library.Clients.IdentityServer;

namespace LuckyFoodSystem.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddHttpClient<IdentityServerClient>();

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
