using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.Contracts.Authentication;
using LuckyFoodSystem.Contracts.Role;
using LuckyFoodSystem.Contracts.User;
using Microsoft.AspNetCore.Identity;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(CreateUserRequest request);

        Task<IdentityResult> UpdateUserAsync(LuckyFoodUser user);

        Task<IdentityResult> RemoveUserAsync(LuckyFoodUser user);

        Task<UserManagementServiceResponse<LuckyFoodUser>> GetUserByNameAsync(string name);

        Task<UserManagementServiceResponse<IEnumerable<LuckyFoodUser>>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<IdentityResult> ChangePasswordAsync(UserPasswordChangeRequest request);

        Task<IdentityResult> AddUserToRoleAsync(AddRemoveRoleRequest request);

        Task<IdentityResult> RemoveUserFromRoleAsync(AddRemoveRoleRequest request);
    }
}
