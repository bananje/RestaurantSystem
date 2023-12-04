using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.UserRolesManagementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using System.Security.Claims;
using Mapster;
using LuckyFoodSystem.Contracts.User;

namespace LuckyFoodSystem.UserRolesApiGrpc.Services
{
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "ApiScope")]
    public class UserApiService : UserService.UserServiceBase
    {
        private readonly UserManager<LuckyFoodUser> _userManager;
        private readonly IPasswordHasher<LuckyFoodUser> passwordHasher;
        private readonly IMapper _mapper;
        public UserApiService(UserManager<LuckyFoodUser> userManager,
                              IMapper mapper,
                              IPasswordHasher<LuckyFoodUser> passwordHasher)
        {
            _userManager = userManager;
            _mapper = mapper;
            this.passwordHasher = passwordHasher;
        }

        public override async Task<ListReply> GetAll(Empty request, ServerCallContext context)
        {
            ListReply listReply = new();

            var usersList = await _userManager.Users
                .Select(item => new UserReply { Email = item.Email, 
                                                Id = item.Id,
                                                UserName = item.UserName,
                                                PhoneNumber = item.PhoneNumber,})
                .ToListAsync(context.CancellationToken);

            listReply.Users.AddRange(usersList);
            return await Task.FromResult(listReply);
        }

        public override async Task<UserReply> GetByName(GetUserByNameRequest request, ServerCallContext context)
        {
            var user = await _userManager.FindByNameAsync(request.Name);

            if(user == null) 
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

            UserReply userReply = new UserReply() { Email = user.Email,
                                                    PhoneNumber = user.PhoneNumber,
                                                    Id = user.Id,
                                                    UserName = user.UserName};

            return await Task.FromResult(userReply);
        }

        public override async Task<UserResponse> Create(CreateUserRequest request, ServerCallContext context)
        {           
            var user = _mapper.Map<LuckyFoodUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password); // создание пользователя

            if(!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Aborted, result.Errors.First().Description));

            // cоздание сlaims для пользователя
            List<Claim> claims = request.Claims.Claim
                .Select(claimModel => new Claim(claimModel.Name, claimModel.Value)).ToList();

            result = await _userManager.AddClaimsAsync(user, claims);
            if (!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Aborted, result.Errors.First().Description));


            return await Task.FromResult(new UserResponse { Code = StatusCode.OK.ToString(),
                                                            Description = "User successfully created"});
        }

        public override async Task<UserResponse> Update(UpdateUserRequest request, ServerCallContext context)
        {
            // поиск юзера по его ID
            var userToBeUpdated = await _userManager.FindByIdAsync(request.Id);
            if (userToBeUpdated is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Selected user not found"));

            var updatedUser = _mapper.Map<LuckyFoodUser>(request); // преобразование запроса в модель юзера
          
            // обновление значений у свойств отслеживаемого юзера через рефлексию
            var updatedProperties = updatedUser.GetType().GetProperties();
            foreach (var property in updatedProperties)
            {
                var newValue = property.GetValue(updatedUser);
                if (newValue is not null)
                {
                    var oldProperty = userToBeUpdated.GetType().GetProperty(property.Name);
                    if (oldProperty is not null && oldProperty.CanWrite)
                    {
                        oldProperty.SetValue(userToBeUpdated, newValue);
                    }
                }
            }

            // обновление текущего юзера
            var updatedResult = await _userManager.UpdateAsync(userToBeUpdated);
            if (!updatedResult.Succeeded)
                throw new RpcException(new Status(StatusCode.Internal, updatedResult.Errors.First().Description));

            // cоздание сlaims для пользователя
            List<Claim> claims = request.Claims.Claim
                .Select(claimModel => new Claim(claimModel.Name, claimModel.Value)).ToList();

            // добавление новых утверждений
            updatedResult = await _userManager.AddClaimsAsync(userToBeUpdated, claims);

            return await Task.FromResult(new UserResponse
            {
                Code = StatusCode.OK.ToString(),
                Description = "User successfully updated"
            });
        }

        public override async Task<UserResponse> Delete(DeleteUserRequest request, ServerCallContext context)
        {
            var user = await _userManager.FindByNameAsync(request.Name);
            if(user is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Selected user not found"));

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Internal, result.Errors.First().Description));

            return await Task.FromResult(new UserResponse { Code = StatusCode.OK.ToString(),
                                                            Description = "User successfully updated"});
        }
        public override async Task<UserResponse> DeleteFromRole(DeleteUserFromRoleRequest request, ServerCallContext context)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Selected user not found"));

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Internal, result.Errors.First().Description));

            return await Task.FromResult(new UserResponse { Code = StatusCode.OK.ToString(),
                                                            Description = "User successfully removed"});
        }
        public override async Task<UserResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            var t = request.CurrentPassword.GetHashCode();
            var user = await _userManager.FindByNameAsync(request.UserName);

            var passwordIsValid = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.CurrentPassword) == PasswordVerificationResult.Success;
            if (user == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Selected user not found"));

            var result = await _userManager
                .ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                throw new RpcException(new Status(StatusCode.Internal, result.Errors.First().Description));

            return await Task.FromResult(new UserResponse { Code = StatusCode.OK.ToString(),
                                                            Description = "User successfully removed"});
        }
    }
}
