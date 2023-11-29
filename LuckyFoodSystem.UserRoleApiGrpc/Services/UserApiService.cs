using Grpc.Core;
using LuckyFoodSystem.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LuckyFoodSystem.UserRolesApiGrpc.Services
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "ApiScope")]
    public class UserApiService : UserService.UserServiceBase
    {
        private readonly UserManager<LuckyFoodUser> _userManager;
        public UserApiService(UserManager<LuckyFoodUser> userManager)
        {
            _userManager = userManager;
        }
        public override Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            return base.CreateUser(request, context);
        }
    }
}
