using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]

public class ExpertController : Controller
{
    private readonly IExpertAppService _expertAppService;
    private readonly ICityAppService _cityAppService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IPictureAppService _pictureAppService;

    public ExpertController(IExpertAppService expertAppService, ICityAppService cityAppService, IPictureAppService pictureAppService, UserManager<User> userManager)
    {
        _expertAppService = expertAppService;
        _cityAppService = cityAppService;
        _pictureAppService = pictureAppService;
        _userManager = userManager;
    }

    public async Task<IActionResult> ExpertList(CancellationToken cancellationToken)
    {
        var experts = await _expertAppService.GetExpertInfoAsync(cancellationToken);
        return View(experts);
    }

    public async Task<IActionResult> DeleteExpert(int userId, CancellationToken cancellationToken)
    {

        var Flag = await _expertAppService.DeleteAsync(userId, cancellationToken);
        if (!Flag)
        {
            TempData["ExpertNotFound"] = "کارشناس یافت نشد";
            return RedirectToAction("ExpertList");
        }

        TempData["DeleteResult"] = "کارشناس با موفقیت حذف گردید";
        return RedirectToAction("ExpertList");
    }

    [HttpGet]
    public async Task<IActionResult> CreateExpert(CancellationToken cancellationToken)
    {

        var cities = await _cityAppService.GetCitiesAsync(cancellationToken);
        ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Title
        }).ToList());
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpert(CreateCustomerViewModel viewModel, CancellationToken cancellationToken)
    {
        var cities = await _cityAppService.GetCitiesAsync(cancellationToken);
        if (!ModelState.IsValid)
        {

            ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList());
            return View(viewModel);
        }


        if (viewModel.ProfileImgFile is not null)
        {
            viewModel.ImagePath = await _pictureAppService.UploadImage(viewModel.ProfileImgFile!, "Experts", cancellationToken);
        }

        var user = new User
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            Address = viewModel.Address,
            UserName = viewModel.UserName,
            CityId = viewModel.CityId,
            PicturePath = viewModel.ImagePath
        };

        var result = await _expertAppService.RegisterAsync(user, viewModel.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList());


        }
        await _userManager.AddToRoleAsync(user, "Expert");
        var expert = new Expert
        {
            UserId = user.Id,
            User = user
        };
        var flag = await _expertAppService.CreateAsync(expert, cancellationToken);
        if (flag)
        {
            TempData["Message"] = "مشتری با موفقیت اضافه شد";    /////////////////بولیناش بررسی شود 

            ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList());
            return View(viewModel);
        }
        TempData["Message"] = "مشتری موجود است ";
        ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Title
        }).ToList());
        return View(viewModel);
    }


    public async Task<IActionResult> ActiveExpert(int userId, CancellationToken cancellationToken)
    {
        var Flag = await _expertAppService.ActiveExpertAsync(userId, cancellationToken);
        if (!Flag)
        {
            TempData["ExpertNotFound"] = "مشتری یافت نشد";
            return RedirectToAction("ExpertList");
        }

        TempData["DeleteResult"] = "مشتری با موفقیت فعال گردید ";
        return RedirectToAction("ExpertList");
    }


    public async Task<IActionResult> UpdateExpert(int userId, CancellationToken cancellationToken)
    {
        var customer = await _expertAppService.GetExpertInfoByIdAsync(userId, cancellationToken);
        if (customer == null)
            return NotFound();        

        var model = new UpdateCustomerViewModel
        {
            Id = customer.Id,
            Email = customer.User.Email,
            FirstName = customer.User.FirstName,
            LastName = customer.User.LastName

        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateExpert(UpdateCustomerViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return NotFound();            

        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;

        var result = await _expertAppService.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        return RedirectToAction("ExpertList", "Expert");

    }
}
