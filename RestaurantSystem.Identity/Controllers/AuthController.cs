using Duende.IdentityServer.Events;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Models.Models;
using RestaurantSystem.Models.VM;
using RestaurantSystem.Identity.Views.Auth;
=======
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
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0

namespace RestaurantSystem.Identity.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;
<<<<<<< HEAD
=======
        private readonly ILogger<AuthController> _logger;
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0

        public AuthController(IIdentityServerInteractionService interaction,
                              IEventService events,
                              UserManager<WebAppUser> userManager,
<<<<<<< HEAD
                              SignInManager<WebAppUser> signInManager)
=======
                              SignInManager<WebAppUser> signInManager,
                              ILogger<AuthController> logger)
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
        {
            _interaction = interaction;
            _events = events;
            _signInManager = signInManager;
            _userManager = userManager;
<<<<<<< HEAD
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

=======
            _logger = logger;
        }

        [BindProperty]
        public LoginVM LoginInputModel { get; set; }

        [BindProperty]
        public RegVM RegInputModel { get; set; }

        public static string ReturnUrl { get; set; }

>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            if (returnUrl != null)
            {
<<<<<<< HEAD
                ReturnUrl = returnUrl;
            }

            return View();
=======
                LoginInputModel = new LoginVM { ReturnUrl = returnUrl };
                ReturnUrl = returnUrl;
            }

            return View(LoginInputModel);
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
        }       

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> LoginAsync(LoginVM loginInputModel)
        {
            var context = await _interaction.GetAuthorizationContextAsync(loginInputModel.ReturnUrl);

            if (loginInputModel.Button != "login")
=======
        public async Task<IActionResult> LoginAsync()
        {
            var context = await _interaction.GetAuthorizationContextAsync(LoginInputModel.ReturnUrl);

            if (LoginInputModel.Button != "login")
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
            {
                if (context != null)
                {
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);                    
                }

                return Redirect("https://localhost:7235/Home");
            }

            if (ModelState.IsValid)
            {
<<<<<<< HEAD
                var user = await _signInManager.UserManager.FindByNameAsync(loginInputModel.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginInputModel.UserName), "Возможно введен неверный логин");
                    return View(loginInputModel);
                }

                if (user != null && (await _signInManager.CheckPasswordSignInAsync(user, loginInputModel.Password, false)) == Microsoft.AspNetCore.Identity.SignInResult.Success)
=======
                var user = await _signInManager.UserManager.FindByNameAsync(LoginInputModel.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(LoginInputModel.UserName), "Возможно введен неверный логин");
                    return View(LoginInputModel);
                }

                if (user != null && (await _signInManager.CheckPasswordSignInAsync(user, LoginInputModel.Password, false)) == Microsoft.AspNetCore.Identity.SignInResult.Success)
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                    AuthenticationProperties props = null;
<<<<<<< HEAD
                    if (LoginOptions.AllowRememberLogin && loginInputModel.RememberLogin)
=======
                    if (LoginOptions.AllowRememberLogin && LoginInputModel.RememberLogin)
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
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
<<<<<<< HEAD
                    ModelState.AddModelError(nameof(loginInputModel.Password), "Введён неверный пароль");
                    loginInputModel.Password = "";
                    return View(loginInputModel);
=======
                    ModelState.AddModelError(nameof(LoginInputModel.Password), "Введён неверный пароль");
                    LoginInputModel.Password = "";
                    return View(LoginInputModel);
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
                }
            }
            else
            {
<<<<<<< HEAD
                return View(loginInputModel);
            }
            
            return Redirect(loginInputModel.ReturnUrl);
=======
                return View(LoginInputModel);
            }
            
            return Redirect(LoginInputModel.ReturnUrl);
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> RegisterAsync(string? returnUrl)
        {
            if (ReturnUrl != null)
            {
<<<<<<< HEAD
                RegVM regInputModel = new RegVM { ReturnUrl = ReturnUrl };
                return View(regInputModel);
            }
            return View();
=======
                RegInputModel = new RegVM { ReturnUrl = ReturnUrl };
            }
            return View(RegInputModel);
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> RegisterAsync(RegVM regInputModel)
        {
            if (regInputModel.Button == "register")
            {
                regInputModel.ReturnUrl = ReturnUrl;
                if (ModelState.IsValid)
                {
                    if (await _userManager.FindByNameAsync(regInputModel.UserName) != null)
                    {
                        ModelState.AddModelError(nameof(regInputModel.UserName), "Логин занят");
                        return View(regInputModel);
                    }

                    var user = new WebAppUser { UserName = regInputModel.UserName, Email = regInputModel.Email };
                   
                    var result = await _userManager.CreateAsync(user, regInputModel.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Customer");
=======
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
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0

                        var isuser = new IdentityServerUser(user.Id)
                        {
                            DisplayName = user.UserName,
                        };
<<<<<<< HEAD
                        await HttpContext.SignInAsync(isuser);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(regInputModel.PhoneNumber), "Возникла необработанная ошибка. Попробуйте позже!");
                        return View(regInputModel);
=======
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
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
                    }
                }
                else
                {
<<<<<<< HEAD
                    return View(regInputModel);
=======
                    return View(RegInputModel);
>>>>>>> c7a13f1b4897e0e2ba4d3ba32db6fe63f5c50ee0
                }
            }

            return Redirect(ReturnUrl);
        }
    }
}
