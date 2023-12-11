using LuckyFoodSystem.API;
using LuckyFoodSystem.Application;
using LuckyFoodSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddInfrastructure(builder.Configuration)
                    .AddApplication();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseStaticFiles();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.UseSession();

    app.Run();
}

