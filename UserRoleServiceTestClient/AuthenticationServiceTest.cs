using Duende.IdentityServer.Models;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using IdentityModel.Client;
using LuckyFoodSystem.Application.Common.Options;
using LuckyFoodSystem.UserRoleManagementService.Infrastructure.Interceptors;
using LuckyFoodSystem.UserRolesManagementService;
using Microsoft.Extensions.Options;
using OnlineShop.Library.Clients.IdentityServer;
using System.Net.Http;
using System.Threading.Channels;

namespace UserRoleServiceTestClient
{
    public class AuthenticationServiceTest
    {
        private readonly IdentityServerClient _identityServerClient;
        private readonly IdentityServerApiOptions _identityServerOptions;

        public AuthenticationServiceTest(
           IdentityServerClient identityServerClient,
           IOptions<IdentityServerApiOptions> options)
        {
            _identityServerClient = identityServerClient;
            _identityServerOptions = options.Value;
        }

        public async Task<string> RunUsersClientTest(string[] args)
        {
            var token = await _identityServerClient.GetApiToken(_identityServerOptions);

            var headers = new Metadata
            {
                { "Authorization", $"Bearer {token.AccessToken}" }               
            };

            using var channel = GrpcChannel.ForAddress("https://localhost:7230");
            var invoker = channel.Intercept(new AuthenticationInterceptor());

            var client = new UserService.UserServiceClient(invoker);
            ListReply users = await client.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty(), headers);

            foreach (var user in users.Users)
            {
                Console.WriteLine($"{user.Id}. {user.UserName} - {user.Email}");
            }
            return "OK";

        }
    }
}
