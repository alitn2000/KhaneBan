using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;


[Area("Users")]
//[Authorize(Roles = "Expert")]
public class ExpertProfileController : Controller
{
    private readonly IExpertAppService _expertAppService;
    private readonly UserManager<User> _userManager;
    public ExpertProfileController(IExpertAppService expertAppService, UserManager<User> userManager)
    {
        _expertAppService = expertAppService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
     {
        var expert = User.FindFirstValue(ClaimTypes.NameIdentifier);                    //////////////amaliyat haye tekrari baresi shavad

        var onlineUser = await _userManager.GetUserAsync(User);

        if (onlineUser is null)
            return RedirectToAction("Login", "Account");
        int userId = int.Parse(expert);

        var userInfo = await _expertAppService.GetExpertProfileByIdAsync(onlineUser.Id, cancellationToken);


        
        return View(userInfo);

    }

    public async Task<IActionResult> ExpertDetail(int expertId, CancellationToken cancellationToken)
    {
        var expert = await _expertAppService.GetExpertProfileByIdAsync(expertId, cancellationToken);
        return View(expert);
    }





}
