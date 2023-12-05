using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LuckyFoodSystem.UserRolesManagementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.UserRoleManagementService.Services
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "ApiScope")]
    public class RoleApiService : RoleService.RoleServiceBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleApiService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public override async Task<RoleResponse> Create(RoleRequest request, ServerCallContext context)
        {
            var role = new IdentityRole { Name = request.Name };
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Internal, result.Errors.First().Description));

            return await Task.FromResult(new RoleResponse { Code = StatusCode.OK.ToString(),
                                                            Description = "Role successfulley created"});
        }
        public override async Task<RoleResponse> Delete(RoleRequest request, ServerCallContext context)
        {
            var role = new IdentityRole { Name = request.Name };
            var selectedRole = await _roleManager.FindByNameAsync(role.Name);

            if(selectedRole is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Selected role not found"));

            var result = await _roleManager.DeleteAsync(selectedRole);
            if (!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Internal, result.Errors.First().Description));

            return await Task.FromResult(new RoleResponse  { Code = StatusCode.OK.ToString(),
                                                             Description = "Role successfully deleted"});
        }
        public override async Task<RoleListReply> GetAll(Empty request, ServerCallContext context)
        {
            RoleListReply reply = new();

            var roleList = await _roleManager.Roles.Select(
                item => new RoleReply { Id = item.Id,
                                        Name = item.Name }).ToListAsync();

            reply.Roles.AddRange(roleList);
            return await Task.FromResult(reply);
        }
        public override async Task<RoleReply> GetByName(RoleRequest request, ServerCallContext context)
        {
            var role = new IdentityRole { Name = request.Name };
            var selectedRole = await _roleManager.FindByNameAsync(role.Name);

            if (selectedRole is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Selected role not found"));

            return await Task.FromResult(new RoleReply { Id= selectedRole.Id,
                                                         Name = selectedRole.Name });
        }
    }
}
