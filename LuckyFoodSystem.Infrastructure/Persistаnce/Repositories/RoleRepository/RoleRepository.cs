using LuckyFoodSystem.Application.Common.Interfaces.Persistence;
using LuckyFoodSystem.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Infrastructure.Persistаnce.Repositories.RoleRepository
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public Task<IdentityResult> AddRoleAsync(IdentityRole role)
            => _roleManager.CreateAsync(role);
        public IEnumerable<IdentityRole> GetAllRolesAsync()
            =>  _roleManager.Roles.AsEnumerable();
        public Task<UserManagementServiceResponse<IdentityRole>> GetRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> RemoveRoleAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateRoleAsync(string name, IdentityRole role)
        {
            throw new NotImplementedException();
        }
    }
}
