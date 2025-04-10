using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAdminAccountAppService _adminAccountAppService;
    private readonly ICustomerAppService _customerAppService;
    private readonly IExpertAppService _expertAppService;
    private readonly ICityAppService _cityAppService;
    private readonly IRequestAppService _requestAppService;
    private readonly ISuggestionAppService _suggestionAppService;
    private readonly IRatingAppService _ratingAppService;
    private readonly IPictureAppService _pictureAppService;
    public AdminController(UserManager<User> userManager,
        IAdminAccountAppService adminAccountAppService,
        SignInManager<User> signInManager,
        ICustomerAppService customerAppService,
        IExpertAppService expertAppService,
        ICityAppService cityAppService,
        IRequestAppService requestAppService,
        ISuggestionAppService suggestionAppService,
        IRatingAppService ratingAppService,
        IPictureAppService pictureAppService)
        
    {
        _userManager = userManager;
        _adminAccountAppService = adminAccountAppService;
        _signInManager = signInManager;
        _customerAppService = customerAppService;
        _expertAppService = expertAppService;
        _cityAppService = cityAppService;
        _requestAppService = requestAppService;
        _suggestionAppService = suggestionAppService;
        _ratingAppService = ratingAppService;
        _pictureAppService = pictureAppService;
    }
    #region AdminLogin
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(AdminViewModel adminViewModel, CancellationToken cancel)
    {
        if (!ModelState.IsValid)
            return View(adminViewModel);
        var result = await _adminAccountAppService.LoginAsync(adminViewModel.UserName, adminViewModel.Password);

        if (!result.Succeeded)
        {
            TempData["LoginResult"] = "شناسه کاربری یا رمز عبور اشتباه است";
            return View(adminViewModel);
        }

            return RedirectToAction("Index", "Home");
    }

    [HttpGet]

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion

   

   

  

    #region Rating
    //----------Rating----------//


    public async Task<IActionResult> RatingList(CancellationToken cancellationToken)
    {
        var ratings = await _ratingAppService.GetRatingsWithDetailsAsync(cancellationToken);
        return View(ratings);
    }

    public async Task<IActionResult> UpdateStatus(int ratingId, bool status, CancellationToken cancellationToken)
    {
        if (status)
        {
            if (await _ratingAppService.Accept(ratingId, cancellationToken))
            {
               ViewBag.UpdateStatus = "تغییر وضعیت با موفقییت ثبت شد";
                return RedirectToAction("RatingList");
            }

            ViewBag.UpdateStatus = "نظر مشتری یافت نشد";
            return RedirectToAction("RatingList");
        }

        else
        {
            if (await _ratingAppService.Reject(ratingId, cancellationToken))
            {
                ViewBag.UpdateStatus = "نظر مشتری رد صلاحیت شد و حذف شد";
                return RedirectToAction("RatingList");
            }



            ViewBag.UpdateStatus = "نظر مشتری یافت نشد";
            return RedirectToAction("RatingList");
        }
        
    }
    #endregion

}
