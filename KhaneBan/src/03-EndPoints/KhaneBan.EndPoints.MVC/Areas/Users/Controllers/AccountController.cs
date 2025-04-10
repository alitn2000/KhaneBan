using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;
[Area("Users")]
public class AccountController : Controller
{
    private readonly IAccountAppService _accountAppService;
    private readonly ICityAppService _cityAppService;

    public AccountController(IAccountAppService accountAppService, ICityAppService cityAppService)
    {
        _accountAppService = accountAppService;
        _cityAppService = cityAppService;
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
    [Route("Users/Account/Register")]
    public async Task<IActionResult> Register(CancellationToken cancellationToken)
    {
        var cities = await _cityAppService.GetCitiesAsync(cancellationToken);

        ViewData["Cities"] = cities.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Title
        }).ToList();
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserDTO model, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                CityId = model.CityId,
                Email = model.Email
            };
            var result = await _accountAppService.RegisterAsync(user, model.Password, model.Role, cancellationToken);
            if (result.Succeeded)
            {
                var cities = await _cityAppService.GetCitiesAsync(cancellationToken);

                ViewData["Cities"] = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList();

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", " ایمیل یا رمز عبور اشتباه است");
            return View(model);

        }
        return View(model);

    }
}