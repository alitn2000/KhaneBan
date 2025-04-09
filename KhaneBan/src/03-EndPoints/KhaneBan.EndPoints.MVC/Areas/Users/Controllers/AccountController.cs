using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.DTOs;
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
    public async Task<IActionResult> Login(UserLoginDTO dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var result = await _accountAppService.Login(dto);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ViewBag.LoginMessage = "ایمیل یا رمزعبور اشتباه است";
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _accountAppService.Logout();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult RegisterAsync()
    {
        return View();
    }



}
