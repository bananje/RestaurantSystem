using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Utils.IServices;
using RestaurantMenu.Utils.Services;
using RestaurantMenu.Utils.Services.Mapper;
using RestaurantSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using RestaurantMenu.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:7082";
                    options.ClientId = WC.ClientsName.MenuWebApp.ToString();
                    options.ClientSecret = builder.Configuration["SecretsKey:IdentityKey"];
                    options.ResponseType = "code";

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");

                    options.SaveTokens = true;

                    options.GetClaimsFromUserInfoEndpoint = true;
                });

builder.Services.AddAuthorization(cfg =>
{
    //cfg.AddPolicy("OlderThan", builder =>
    //{
    //    builder.AddRequirements(new OlderThanRequirment(10));
    //});
    //cfg.AddPolicy("Admin", opt =>
    //{
    //    opt.AddRequirements(new AdminRequirment(builder.Configuration["SecretsKey:AdminKey"]));
    //});
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RestaurantSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(25);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}); 

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IHttpContextProvider, HttpContextProvider>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IValidatorService, ValidatorService>();
builder.Services.AddSingleton(MapperConfigure.InitializeAutoMapper());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages().RequireAuthorization();

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();


// пример реализации политики с логикой проверки

//public class OlderThanRequirment : IAuthorizationRequirement
//{
//    public OlderThanRequirment(int years) => Years = years;
//    public int Years { get;}
//}

//public class OlderThanRequirmentHandler : AuthorizationHandler<OlderThanRequirment>
//{    
//    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OlderThanRequirment requirement)
//    {
//        var hasClaim = context.User.HasClaim(x => x.Type == ClaimTypes.DateOfBirth);
//        if (!hasClaim)
//        {
//            return Task.CompletedTask;
//        }

//        var dateOfBirth = context.User.FindFirst(x => x.Type == ClaimTypes.DateOfBirth).Value;
//        var date = DateTime.Parse(dateOfBirth, new CultureInfo("ru-RU"));
//        var diff = DateTime.Now.Year - date.Year;
//        if(diff >= requirement.Years)
//        {
//            context.Succeed(requirement);
//        }

//        return Task.CompletedTask;
//    }
//}