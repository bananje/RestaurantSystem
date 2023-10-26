using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models.Models;

namespace RestaurantSystem.Identity.Pages.Create;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly TestUserStore _users;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly UserManager<WebAppUser> _userManager;

    [BindProperty]
    public InputModel Input { get; set; }

    [BindProperty]
    public bool CanClose { get; set; }
        
    public Index(
        IIdentityServerInteractionService interaction,
        UserManager<WebAppUser> userManager)
    {       
        _interaction = interaction;
        _userManager = userManager;
        CanClose = false;
    }

    public IActionResult OnGet(string returnUrl)
    {        
        return Page();
    }
        
    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request        

        if(Input.Button == "create")
        {
            if (await _userManager.FindByNameAsync(Input.Username) != null)
            {
                ModelState.AddModelError("Input.Username", "Invalid username");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new WebAppUser { UserName = Input.Username, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var isuser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.UserName
                    };
                    await HttpContext.SignInAsync(isuser);

                    string? url = HttpContext.Session.GetString("url");
                    if (url != null)
                    {
                        return Redirect(url.ToString());
                    }

                    return Redirect("https://localhost:7235/Home");
                }
                else
                {
                    ModelState.AddModelError("Input.Email", "Invalid input data");
                    return Page();
                }         
            }
        }
        else
        {
            CanClose = true;
            return Redirect("https://localhost:7235/Home");
        }

        return Page();
    }
}