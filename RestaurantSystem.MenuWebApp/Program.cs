using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Utils.IServices;
using RestaurantMenu.Utils.Services;
using RestaurantMenu.Utils.Services.Mapper;
using RestaurantSystem.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
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

                    options.TokenValidationParameters.RoleClaimType = "role";
                    options.Scope.Add("Menu");

                    options.SaveTokens = true;

                    options.GetClaimsFromUserInfoEndpoint = true;
<<<<<<< HEAD
                    options.ClaimActions.MapJsonKey("role", "role");
=======
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
                });

builder.Services.AddAuthorization();

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

app.MapRazorPages().RequireAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
