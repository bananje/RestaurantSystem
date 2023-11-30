using LuckyFoodSystem.Application.Common.Models;
using LuckyFoodSystem.UserRolesManagementService;
using Mapster;

namespace LuckyFoodSystem.UserRoleManagementService.Application.Mappings
{
    public class UserClaimsMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateUserRequest, LuckyFoodUser>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber);

            config.NewConfig<UpdateUserRequest, LuckyFoodUser>()
                .Map(dest => dest, src => src.User);

        }
    }
}
