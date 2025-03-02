using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.EndPoints.MVC.Areas.Users.Models;
using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;
[Area("Users")]
public class AccountController : Controller
{
    private readonly IAccountAppService _accountAppService;

    public AccountController(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }

    [HttpGet]
    public IActionResult Login()                                         
                                                                           
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var dto = new UserLoginDTO()
        {
            Email = viewModel.Email,
            Password = viewModel.Password,
            IsPresistent = viewModel.IsPresistent,
            Role = viewModel.Role
        };

        var result = await _accountAppService.Login(dto);

        if (result.Succeeded)
           return RedirectToAction("Index", "Home");

        ViewBag.LoginMessage = "ایمیل یا رمزعبور اشتباه است";
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _accountAppService.Logout();
        return RedirectToAction("Index", "Home");
    }

}
