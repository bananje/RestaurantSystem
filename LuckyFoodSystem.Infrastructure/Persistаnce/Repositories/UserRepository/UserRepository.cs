using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.Contracts.Authentication;
using LuckyFoodSystem.Contracts.Role;
using LuckyFoodSystem.Contracts.User;
using Microsoft.AspNetCore.Identity;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public Task<IdentityResult> AddUserToRoleAsync(AddRemoveRoleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ChangePasswordAsync(UserPasswordChangeRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateUserAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UserManagementServiceResponse<IEnumerable<LuckyFoodUser>>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserManagementServiceResponse<LuckyFoodUser>> GetUserByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoveUserAsync(LuckyFoodUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoveUserFromRoleAsync(AddRemoveRoleRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateUserAsync(LuckyFoodUser user)
        {
            throw new NotImplementedException();
        }
    }
}
