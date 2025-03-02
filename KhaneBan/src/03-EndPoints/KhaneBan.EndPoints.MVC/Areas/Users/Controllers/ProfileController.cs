using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.EndPoints.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KhaneBan.EndPoints.MVC.Areas.Users;

[Area("Users")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ICustomerAppService _customerAppService;
    private readonly ICityAppService _cityAppService;
    private readonly IPictureAppService _pictureAppService;

    public ProfileController(UserManager<User> userManager, ICustomerAppService customerAppService, ICityAppService cityAppService, IPictureAppService pictureAppService)
    {
        _userManager = userManager;
        _customerAppService = customerAppService;
        _cityAppService = cityAppService;
        _pictureAppService = pictureAppService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);                    //////////////amaliyat haye tekrari baresi shavad


        if (onlineUserId is null)
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(onlineUserId);

      var userInfo =  await _customerAppService.GetCustomerByIdWithDetailsAsync(userId, cancellationToken);


        return View(userInfo);


    }


    public async  Task<IActionResult> UpdateCustomerProfile(CancellationToken cancellationToken)
    {
        var cities = await _cityAppService.GetCitiesAsync(cancellationToken);
        ViewBag.Cities = cities.Select(sc => new SelectListItem
        {
            Value = sc.Id.ToString(),
            Text = sc.Title
        }).ToList();
        var onlineUser =await _userManager.GetUserAsync(User);                                                                //////////////amaliyat haye tekrari baresi shavad


        if (onlineUser is null)
            return RedirectToAction("Login", "Account");

        var model = new UpdateCustomerInfoViewModel
        {
            FirstName = onlineUser.FirstName,
            LastName = onlineUser.LastName,
            Email = onlineUser.Email,
            Address = onlineUser.Address,
            ImagePath = onlineUser.PicturePath,
            PhoneNumber = onlineUser.PhoneNumber,
            CityId = onlineUser.CityId,
            UserName = onlineUser.UserName
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCustomerProfile(UpdateCustomerInfoViewModel model, CancellationToken cancellationToken)
    {
        var cities = await _cityAppService.GetCitiesAsync(cancellationToken);

        if (!ModelState.IsValid)
        {
            ViewBag.Cities = cities.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Title
            }).ToList();
            return View(model);

        }

      if (model.ImageFile is not null)
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "Customers", cancellationToken);
        }

        var onlineUser = await _userManager.GetUserAsync(User);                                                                //////////////amaliyat haye tekrari baresi shavad

        if (onlineUser is null)
            return RedirectToAction("Login", "Account");

        onlineUser.FirstName = model.FirstName;
        onlineUser.LastName = model.LastName;
        onlineUser.Email = model.Email;
        onlineUser.Address = model.Address;
        onlineUser.PicturePath = model.ImagePath;
        onlineUser.PhoneNumber = model.PhoneNumber;
        onlineUser.CityId = model.CityId;
        onlineUser.UserName = model.UserName;
        ///////password ro ham update konim
       var result = await _customerAppService.UpdateAsync(onlineUser);

        if (result.Succeeded)
        {
            ViewBag.UpdateMessage = "ویرایش اطلاعات با موفقیت انجام شد";
            return RedirectToAction("UpdateCustomerProfile");
        }
        ViewBag.UpdateMessage = "ویرایش اطلاعات انجام نشد";

        return RedirectToAction("UpdateCustomerProfile");
    }



    public IActionResult SuggestionList()
    {
        return View();
    }

    public IActionResult RequestList()
    {
        return View();
    }
}
