using LuckyFoodSystem.Infrastructure;
using LuckyFoodSystem.UserRoleApiGrpc;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddUsersPresentation(builder.Configuration)
                    .AddPersistance();
}

var app = builder.Build();
{
    app.UseAuthentication();
    app.UseAuthorization();

    app.Run();
}


