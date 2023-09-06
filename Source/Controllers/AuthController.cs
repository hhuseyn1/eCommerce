using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Encryptors;
using Source.Models;
using Source.Models.ViewModels;
using Source.Services;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Source.Controllers;

public class AuthController : Controller
{
    private readonly AppDbContext _context;
    private readonly IUserManager manager;

    public AuthController(AppDbContext context, IUserManager manager)
    {
        this._context = context;
        this.manager = manager;
    }

    public IActionResult Register() => View();

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (manager.Register(viewModel.Login, viewModel.Password, false))
                    return RedirectToAction("Login");
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
                if (manager.Login(viewModel.Login, viewModel.Password))
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("All", "Invalid username or password!");
            }
            return View();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}
