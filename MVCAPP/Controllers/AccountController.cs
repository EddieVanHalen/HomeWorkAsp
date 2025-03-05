using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAPP.DataAccess;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;
using MVCAPP.Models;

namespace MVCAPP.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApiUser> _userManager;

    private readonly SignInManager<ApiUser> _signInManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ApiUser> userManager, SignInManager<ApiUser> signinManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signinManager;
        _roleManager = roleManager;
    }

    #region Registration

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO register)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid model state";
            return RedirectToAction(nameof(Register));
        }

        ApiUser appUser = new ApiUser
        {
            UserName = register.Email,
            Email = register.Email
        };

        IdentityResult result = await _userManager.CreateAsync(appUser, register.Password);
        
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
            TempData["Error"] = "Error while saving the user";
            return RedirectToAction(nameof(Register));
        }
        
        // await _roleManager.CreateAsync(new IdentityRole("User"));
        await _userManager.AddToRoleAsync(appUser, "User");

        TempData["Success"] = "User added successfully";
        return RedirectToAction("Index", "Home");
    }

    #endregion
    
    
    #region Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO login)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Invalid model state";
            return RedirectToAction(nameof(Login));
        }

        ApiUser? appUser = await _userManager.FindByEmailAsync(login.Email);

        if (appUser is null)
        {
            TempData["Error"] = "User not found";
            return RedirectToAction(nameof(Login));
        }

        bool isChecked = await _userManager.CheckPasswordAsync(appUser, login.Password);

        if (!isChecked)
        {
            TempData["Error"] = "Wrong credentials";
            return RedirectToAction(nameof(Login));
        }

        await _signInManager.SignInAsync(appUser,false);

        TempData["Success"] = "User Signed In successfully";
        return RedirectToAction("Index", "Home");
    }

    #endregion
}