using LuckyFoodSystem.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;

namespace LuckyFoodSystem.Application.Common.Interfaces.Persistence
{
    public interface IRoleRepository
    {
        Task<IdentityResult> AddRoleAsync(IdentityRole role);
        Task<UserManagementServiceResponse<IdentityRole>> GetRoleByNameAsync(string name);
        IEnumerable<IdentityRole> GetAllRolesAsync();
        Task<IdentityResult> RemoveRoleAsync(string name);
        Task<IdentityResult> UpdateRoleAsync(string name, IdentityRole role);
    }
}
