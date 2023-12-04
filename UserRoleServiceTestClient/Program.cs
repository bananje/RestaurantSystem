using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Library.Clients.IdentityServer;
using Microsoft.Extensions.Logging;
using LuckyFoodSystem.Application.Common.Options;
using UserRoleServiceTestClient;
using LuckyFoodSystem.Application.Common.Mapping;

namespace tests.UserRoleServiceTestClient
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<IdentityServerClient>();

                    services.AddTransient<AuthenticationServiceTest>();

                    services.AddMappings();

                    var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false);

                    IConfiguration configuration = configurationBuilder.Build();

                    services.Configure<IdentityServerApiOptions>(configuration.GetSection(IdentityServerApiOptions.SectionName));
                    services.Configure<ServiceAdressOptions>(configuration.GetSection(ServiceAdressOptions.SectionName));
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var service = services.GetRequiredService<AuthenticationServiceTest>();

                    var usersResult = await service.RunUsersClientTest(args);

                    Console.WriteLine(usersResult);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
            }
            Console.ReadKey();

            return 0;
        }
    }
}
