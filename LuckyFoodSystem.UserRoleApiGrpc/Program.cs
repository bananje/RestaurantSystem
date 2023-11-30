using LuckyFoodSystem.Application.Common.Mapping;
using LuckyFoodSystem.UserRoleApiGrpc;
using LuckyFoodSystem.UserRolesApiGrpc.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddUsersPresentation(builder.Configuration)
                    .AddMappings();
}

var app = builder.Build();
{
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGrpcService<UserApiService>();

    app.Run();
}


