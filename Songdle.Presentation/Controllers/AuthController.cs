using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Songdle.Presentation.Controllers;

public class AuthController(SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager) : Controller
{
    



    [HttpGet("/auth/login")]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = "/")
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("/auth/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password, string returnUrl = "/")
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Podaj login i hasło.");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }

        ModelState.AddModelError("", "Nieprawidłowy login lub hasło.");
        return View();
    }

    [HttpPost("/auth/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}
