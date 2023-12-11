var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//                {
//                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//                })
//                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
//                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
//                {
//                    options.Authority = "https://localhost:7082";
//                    options.ClientId = WC.ClientsName.MenuWebApp.ToString();
//                    options.ClientSecret = builder.Configuration["SecretsKey:IdentityKey"];
//                    options.ResponseType = "code";

//                    options.Scope.Clear();
//                    options.Scope.Add("openid");
//                    options.Scope.Add("profile");

//                    options.TokenValidationParameters.RoleClaimType = "role";
//                    options.Scope.Add("Menu");

//                    options.SaveTokens = true;

//                    options.GetClaimsFromUserInfoEndpoint = true;
//                    options.ClaimActions.MapJsonKey("role", "role");
//                });

//builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(25);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}); 


var app = builder.Build();

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
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

app.Run();
