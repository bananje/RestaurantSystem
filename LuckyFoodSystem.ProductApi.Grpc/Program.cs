using LuckyFoodSystem.ProductApi.Grpc;
using LuckyFoodSystem.ProductApi.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddGrpcModule();
}

var app = builder.Build();
{
    app.MapGrpcService<ProductApiService>();   
    app.Run();
}

