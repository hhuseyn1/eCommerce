using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Source.Models;
using Source.Models.ViewModels;
using Source.Services;

namespace Source.Controllers;

public class AuthController : Controller
{
    private readonly IUserManager manager;
    private readonly SignInManager<AppUser> signInManager;

    public AuthController(IUserManager manager, SignInManager<AppUser> signInManager)
    {
        this.manager = manager;
        this.signInManager = signInManager;
    }

    public IActionResult Register() => View();

    public IActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    UserName = viewModel.Login,
                    Email = viewModel.Email
                };
                if (manager.Register(viewModel.Login, viewModel.Password, false))
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login");
                }
            }
            ModelState.AddModelError("All", "Login is allready taken");
            return View();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    UserName = viewModel.Login,
                    Email = viewModel.Email
                };
                if (manager.Login(viewModel.Login, viewModel.Password))
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("All", "Invalid username or password!");
            }
            return View();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
