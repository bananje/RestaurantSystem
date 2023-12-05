using Duende.IdentityServer;
using LuckyFoodSystem.Application.Common.Constants;
using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.Identity.Data;
using LuckyFoodSystem.Identity.Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LuckyFoodSystem.Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var identityServerConnection = builder.Configuration
            .GetConnectionString(ConnectionNames.IdentityServerConnection);

        var assembly = typeof(Program).Assembly.GetName().Name;      

        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<LuckyFoodIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration
                                    .GetConnectionString(ConnectionNames.UserDbConnection)));

        builder.Services.AddIdentity<LuckyFoodUser, IdentityRole>()
            .AddEntityFrameworkStores<LuckyFoodIdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                b.UseSqlServer(identityServerConnection, opt => opt.MigrationsAssembly(assembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                b.UseSqlServer(identityServerConnection, opt => opt.MigrationsAssembly(assembly));
            })
            .AddAspNetIdentity<LuckyFoodUser>();
        
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        IdentityServerInitializer.InitializeDatabase(app);
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}