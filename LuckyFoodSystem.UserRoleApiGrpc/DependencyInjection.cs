using LuckyFoodSystem.Application.Common.Constants;
using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.Identity.Data;
using LuckyFoodSystem.UserRoleManagementService.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.UserRoleApiGrpc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersPresentation(this IServiceCollection services,
                                                              IConfiguration configuration)
        {
            services.AddDbContext<LuckyFoodIdentityDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString(ConnectionNames.UserDbConnection)));

            services.AddIdentity<LuckyFoodUser, IdentityRole>()
                .AddEntityFrameworkStores<LuckyFoodIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "https://localhost:5001/resources";
                    options.RequireHttpsMetadata = true;
                });

            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.Interceptors.Add<AuthenticationInterceptor>();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", Scopes.LuckyFoodSystemApi);
                });
            });

            return services;
        }
    }
}
