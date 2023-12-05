using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using LuckyFoodSystem.Application.Common.Options;
using LuckyFoodSystem.Contracts.User;
using LuckyFoodSystem.UserRoleManagementService.Infrastructure.Interceptors;
using LuckyFoodSystem.UserRoleServiceTestClient;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using OnlineShop.Library.Clients.IdentityServer;
using System.Text;

namespace UserRoleServiceTestClient
{
    // Интеграционное тестирование UsersRolesApi c помощью grpc-клиента
    public class AuthenticationServiceTest
    {
        private readonly IdentityServerClient _identityServerClient;
        private readonly IdentityServerApiOptions _identityServerOptions;
        private readonly IMapper _mapper;

        public AuthenticationServiceTest(
           IdentityServerClient identityServerClient,
           IOptions<IdentityServerApiOptions> options,
           IMapper mapper)
        {
            _identityServerClient = identityServerClient;
            _identityServerOptions = options.Value;
            _mapper = mapper;


            TypeAdapterConfig.GlobalSettings
                .NewConfig<UserRequestDto, CreateUserRequest>()
                .Map(dest => dest, src => src)
                .Ignore(dest => dest.Claims);

            TypeAdapterConfig.GlobalSettings
               .NewConfig<UserRequestDto, UpdateUserRequest>()
               .Map(dest => dest, src => src)
               .Map(dest => dest.Id, src => src.id)
               .Ignore(dest => dest.Claims);
        }

        private ClaimsList ClaimsList(UserRequestDto request)
        {
            var claimsFromRequest = new ClaimsList();
            foreach (var claim in request.Claims)
            {
                claimsFromRequest.Claim.Add(new ClaimModel { Name = claim.Key, Value = claim.Value });
            }

            return claimsFromRequest;
        }

        // тестирование методов для работы пользователями
        public async Task<string> RunUsersClientTest(string[] args)
        {           
            var token = await _identityServerClient.GetApiToken(_identityServerOptions);

            var headers = new Metadata { { "Authorization", $"Bearer {token.AccessToken}" } };

            using var channel = GrpcChannel.ForAddress("https://localhost:7230");

            var invoker = channel.Intercept(new AuthenticationInterceptor());

            var client = new UserService.UserServiceClient(invoker);

            // --- получение всех пользоватей из API ---
            ListReply users = await client.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty(), headers);

            foreach (var user in users.Users)
            {
                Console.WriteLine($"{user.Id}. {user.UserName} - {user.Email}");
            }

            Thread.Sleep(100);

            // --- добавление нового пользователя через вызов в API ----

            var clientRequestForAddingUser = new UserRequestDto(
                "testuser123",
                "test@mail.ru",
                false,
                "test@p!assword~124AZ",
                "3453453453",
                new Dictionary<string, string> { { "testclaimtype", "testclaimvalue" },
                                               { "testclaimtype1", "testclaimvalue1" }}); // тестовый запрос из вне          

            // запрос для grpc API
            var apiRequestForCreateUser = _mapper.Map<CreateUserRequest>(clientRequestForAddingUser);

            // добавление claims к запросу и их нормализация
            var claimsFromRequestForAddingUser = ClaimsList(clientRequestForAddingUser);
            apiRequestForCreateUser.Claims = claimsFromRequestForAddingUser;

            // получение результата создания нового юзера
            UserResponse createResult = client.Create(apiRequestForCreateUser, headers);
            Console.WriteLine("Result of creating new user: " + createResult);

            Thread.Sleep(100);

            // --- получение пользователя по его имени ----
            string userNameForGettingByName = "testuser123";

            GetUserByNameRequest getByNameRequest = new() { Name = userNameForGettingByName };

            UserReply selectedUser = client.GetByName(getByNameRequest, headers);
            Console.WriteLine($"User with name {userNameForGettingByName}: " + selectedUser);

            Thread.Sleep(100);

            // --- обновление пользователя через вызов в API ----

            var clientRequestForUpdatingUser = new UserRequestDto(
                "updatedtestuser",
                "testupdated@mail.ru",
                false,
                "test@p!asswordupdated~124AZ",
                "3453453453",
                new Dictionary<string, string> { { "testclaimtypeupdated", "testclaimvalueup" },
                                               { "testclaimtype1updated", "testclaimvalue1up" }},
                selectedUser.Id);

            var apiRequestForUpdatingUser = _mapper.Map<UpdateUserRequest>(clientRequestForUpdatingUser);

            var claimsFromRequestForUpdatingUser = ClaimsList(clientRequestForUpdatingUser);
            apiRequestForUpdatingUser.Claims = claimsFromRequestForAddingUser;

            UserResponse updateUserResponse = await client.UpdateAsync(apiRequestForUpdatingUser, headers);
            Console.WriteLine("Result of updating user: " + updateUserResponse);

            Thread.Sleep(100);

            // --- изменение текущего пароля ---

            var changePasswordRequest = new UserPasswordChangeRequestDto(
               clientRequestForUpdatingUser.UserName,
               "test@p!assword~124AZ",
               "newPassw0rd!_!fdg345");

            var apiChangePasswordRequest = _mapper.Map<ChangePasswordRequest>(changePasswordRequest);

            UserResponse changePasswordResult = await client.ChangePasswordAsync(apiChangePasswordRequest, headers);
            Console.WriteLine("Result of changing user password: " + changePasswordRequest);

            Thread.Sleep(100);

            // --- удаление пользователя по его ID --- 

            string userNameForDeleting = "updatedtestuser";
            DeleteUserRequest deleteUserRequest = new() { Name = userNameForDeleting };

            // запрос к API на удаление пользователя с указанным именем
            UserResponse deletingUserResult = await client.DeleteAsync(deleteUserRequest, headers);
            Console.WriteLine("Result of deleting selected user: " + deletingUserResult);

            Thread.Sleep(100);

            return "User Tests: OK";
        }

        public async Task<string> RunRolesClientTest(string[] args)
        {
            var token = await _identityServerClient.GetApiToken(_identityServerOptions);

            var headers = new Metadata { { "Authorization", $"Bearer {token.AccessToken}" } };

            using var channel = GrpcChannel.ForAddress("https://localhost:7230");

            var invoker = channel.Intercept(new AuthenticationInterceptor());

            var client = new RoleService.RoleServiceClient(invoker);

            /// --- Создание ролей ---
            RoleRequest firstTestRole = new() { Name = "testRole0" };
            RoleRequest secondTestRole = new() { Name = "testRole1" };

            var creatingResult1 = await client.CreateAsync(firstTestRole, headers);
            Console.WriteLine("Result of creating first test role: " + creatingResult1);

            var creatingResult2 = await client.CreateAsync(secondTestRole, headers);
            Console.WriteLine("Result of creating second test role: " + creatingResult2);

            Thread.Sleep(100);

            /// --- Получение всех ролей ---
            RoleListReply rolesReply = await client.GetAllAsync(new Google.Protobuf.WellKnownTypes.Empty(), headers);
            foreach (var role in rolesReply.Roles)
            {
                Console.WriteLine($"UserName: {role.Name} \n Id: {role.Id}");
            }
            Thread.Sleep(100);

            /// --- Получение роли по имени ---
            var selectedByNameRole = await client.GetByNameAsync(firstTestRole, headers);
            Console.WriteLine($"Role: UserName: {selectedByNameRole.Name} \n Id: {selectedByNameRole.Id}");

            Thread.Sleep(100);

            /// --- Удаление роли по имени ---
            
            var deletingResult1 = await client.DeleteAsync(firstTestRole, headers);
            Console.WriteLine("Result of deleting first test role: " + deletingResult1);

            var deletingResult2 = await client.DeleteAsync(secondTestRole, headers);
            Console.WriteLine("Result of deleting second test role: " + deletingResult2);

            Thread.Sleep(100);

            return "Roles test: OK";
        }
    }
}
