using LuckyFoodSystem.Application.Common.Mapping;
using LuckyFoodSystem.UserRoleApiGrpc;
using LuckyFoodSystem.UserRoleManagementService.Services;
using LuckyFoodSystem.UserRolesApiGrpc.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddUsersPresentation(builder.Configuration)
                    .AddMappings();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGrpcService<UserApiService>();
    app.MapGrpcService<RoleApiService>();

    app.Run();
}


