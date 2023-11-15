using BuberDinner.Api.Mapping;
using FluentValidation;
using LuckyFoodSystem.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace LuckyFoodSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddMappings();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaivor<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
