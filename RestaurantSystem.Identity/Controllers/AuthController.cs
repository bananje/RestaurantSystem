using Duende.IdentityServer.Events;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Models.Models;
using RestaurantSystem.Models.VM;
using RestaurantSystem.Identity.Views.Auth;
using System.Security.Claims;

namespace RestaurantSystem.Identity.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IIdentityServerInteractionService interaction,
                              IEventService events,
                              UserManager<WebAppUser> userManager,
                              SignInManager<WebAppUser> signInManager,
                              ILogger<AuthController> logger)
        {
            _interaction = interaction;
            _events = events;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public static string ReturnUrl { get; set; }

        [Route("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logOutResult = await _interaction.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logOutResult.PostLogoutRedirectUri))
            {
                return Redirect("https://localhost:7235/Home");
            }
            return Redirect(logOutResult.PostLogoutRedirectUri);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            if (returnUrl != null)
            {
                LoginVM model = new() { ReturnUrl = returnUrl };
                ReturnUrl = returnUrl;
                return View(model);
            }
            return View();
        }       

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginVM loginInputModel)
        {
            var context = await _interaction.GetAuthorizationContextAsync(loginInputModel.ReturnUrl);

            if (loginInputModel.Button != "login")
            {
                if (context != null)
                {
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);                    
                }

                return Redirect("https://localhost:7235/Home");
            }

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(loginInputModel.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginInputModel.UserName), "Возможно введен неверный логин");
                    return View(loginInputModel);
                }

                if (user != null && (await _signInManager.CheckPasswordSignInAsync(user, loginInputModel.Password, false)) == Microsoft.AspNetCore.Identity.SignInResult.Success)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                    AuthenticationProperties props = null;
                    if (LoginOptions.AllowRememberLogin && loginInputModel.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(LoginOptions.RememberMeLoginDuration)
                        };
                    };

                    var isuser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.UserName
                    };
                    await HttpContext.SignInAsync(isuser, props);
                }
                else
                {
                    ModelState.AddModelError(nameof(loginInputModel.Password), "Введён неверный пароль");
                    loginInputModel.Password = "";
                    return View(loginInputModel);
                }
            }
            else
            {
                return View(loginInputModel);
            }
            
            return Redirect(loginInputModel.ReturnUrl);
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> RegisterAsync(string? returnUrl)
        {
            if (ReturnUrl != null)
            {
                RegVM model = new RegVM { ReturnUrl = ReturnUrl };
                return View(model);
            }
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegVM regInputModel)
        {
            if (regInputModel.Button == "register")
            {               
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByNameAsync(regInputModel.UserName) != null)
                    {
                        ModelState.AddModelError(nameof(regInputModel.UserName), "Логин занят");
                        return View(regInputModel);
                    }

                    var user = new WebAppUser { UserName = regInputModel.UserName, Email = regInputModel.Email };
                    var claim = new Claim("SecretKey", "bfe11443-382e-4fd2-9683-38786e48937b");
                   
                    var result = await _userManager.CreateAsync(user, regInputModel.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, claim);

                        var isuser = new IdentityServerUser(user.Id)
                        {
                            DisplayName = user.UserName,
                        };
                        await HttpContext.SignInAsync(isuser);                        
                    }
                    else
                    {

                        foreach (var item in result.Errors)
                        {
                            _logger.LogError(item.Description);
                        }

                        ModelState.AddModelError(nameof(regInputModel.PhoneNumber), "Возникла необработанная ошибка. Попробуйте позже!");
                        return View(regInputModel);
                    }
                }
                else
                {
                    return View(regInputModel);
                }
            }

            return Redirect(ReturnUrl);
        }
    }
}
