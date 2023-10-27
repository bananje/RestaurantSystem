using Duende.IdentityServer.Events;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Identity.Pages;
using RestaurantSystem.Models.Models;
using RestaurantSystem.Models.VM;
using RestaurantSystem.Identity.Views.Auth;
using System.Drawing;
using RestaurantMenu.Utils.IServices;
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

        [BindProperty]
        public LoginVM LoginInputModel { get; set; }

        [BindProperty]
        public RegVM RegInputModel { get; set; }

        public static string ReturnUrl { get; set; }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            if (returnUrl != null)
            {
                LoginInputModel = new LoginVM { ReturnUrl = returnUrl };
                ReturnUrl = returnUrl;
            }

            return View(LoginInputModel);
        }       

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync()
        {
            var context = await _interaction.GetAuthorizationContextAsync(LoginInputModel.ReturnUrl);

            if (LoginInputModel.Button != "login")
            {
                if (context != null)
                {
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);                    
                }

                return Redirect("https://localhost:7235/Home");
            }

            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.FindByNameAsync(LoginInputModel.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(LoginInputModel.UserName), "Возможно введен неверный логин");
                    return View(LoginInputModel);
                }

                if (user != null && (await _signInManager.CheckPasswordSignInAsync(user, LoginInputModel.Password, false)) == Microsoft.AspNetCore.Identity.SignInResult.Success)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                    AuthenticationProperties props = null;
                    if (LoginOptions.AllowRememberLogin && LoginInputModel.RememberLogin)
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
                    ModelState.AddModelError(nameof(LoginInputModel.Password), "Введён неверный пароль");
                    LoginInputModel.Password = "";
                    return View(LoginInputModel);
                }
            }
            else
            {
                return View(LoginInputModel);
            }
            
            return Redirect(LoginInputModel.ReturnUrl);
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> RegisterAsync(string? returnUrl)
        {
            if (ReturnUrl != null)
            {
                RegInputModel = new RegVM { ReturnUrl = ReturnUrl };
            }
            return View(RegInputModel);
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync()
        {
            if (RegInputModel.Button == "register")
            {               
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByNameAsync(RegInputModel.UserName) != null)
                    {
                        ModelState.AddModelError(nameof(RegInputModel.UserName), "Логин занят");
                        return View(RegInputModel);
                    }

                    var user = new WebAppUser { UserName = RegInputModel.UserName, Email = RegInputModel.Email };
                    var claim = new Claim("SecretKey", "bfe11443-382e-4fd2-9683-38786e48937b");
                   
                    var result = await _userManager.CreateAsync(user, RegInputModel.Password);
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

                        ModelState.AddModelError(nameof(RegInputModel.PhoneNumber), "Возникла необработанная ошибка. Попробуйте позже!");
                        return View(RegInputModel);
                    }
                }
                else
                {
                    return View(RegInputModel);
                }
            }

            return Redirect(ReturnUrl);
        }
    }
}
