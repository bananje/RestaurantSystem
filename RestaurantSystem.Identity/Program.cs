using Duende.IdentityServer.Services;
using IdentityServerAspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Utils.IServices;
using RestaurantMenu.Utils.Services;
using RestaurantSystem.Identity;
using RestaurantSystem.Models;
using RestaurantSystem.Models.Models;


var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var migationAssembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddDbContext<RestaurantSystemIdentityDb>(options =>
                {
                    options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migationAssembly));
                })
                .AddIdentity<WebAppUser, IdentityRole>(options =>
                {
                    options.Password.RequireLowercase = true; 
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<RestaurantSystemIdentityDb>();

builder.Services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = false;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddConfigurationStore(options => options.ConfigureDbContext = b
                                                => b.UseSqlServer(connectionString, opt
                                                => opt.MigrationsAssembly(migationAssembly)))
                .AddOperationalStore(options => options.ConfigureDbContext = b
                                                => b.UseSqlServer(connectionString, opt
                                                => opt.MigrationsAssembly(migationAssembly)))
                .AddAspNetIdentity<WebAppUser>()
                .AddProfileService<SampleProfileService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddTransient<IValidatorService, ValidatorService>();
builder.Services.AddTransient<IProfileService, SampleProfileService>();

var app = builder.Build();

app.UseIdentityServer();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.UseSession();

if (args.Contains("/seed"))
{
    SeedData.EnsureSeedData(app);
    return;
}

app.Run();
