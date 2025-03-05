using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Contracts.Service;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.EndPoints.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;
[Area("Users")]
[Authorize(Roles = "Customer")]
public class RequestController : Controller
{
    private readonly IRequestAppService _requestAppService;
    private readonly IPictureService _pictureService;
    private readonly UserManager<User> _userManager;
    private readonly ICustomerAppService _customerAppService;
    private readonly IHomeServiceAppService _homeServiceAppService;

    public RequestController(IRequestAppService requestAppService,
        IPictureService pictureService,
        UserManager<User> userManager,
        ICustomerAppService customerAppService,
        IHomeServiceAppService homeServiceAppService)

    {
        _requestAppService = requestAppService;
        _pictureService = pictureService;
        _userManager = userManager;
        _customerAppService = customerAppService;
        _homeServiceAppService = homeServiceAppService;
    }

    [HttpGet]
    public async Task<IActionResult> AddRequest(int homeServiceId, CancellationToken cancellationToken)
    {
        var homeService = await _homeServiceAppService.GetByIdAsync(homeServiceId, cancellationToken);
        var request = new AddRequestViewModel()
        {
            HomeServiceId = homeService.Id,
            HomeServiceTitle = homeService.Title

        };
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> AddRequest(AddRequestViewModel model, CancellationToken cancellationToken)
    {

        if (!ModelState.IsValid)
        {

            return View(model);
        }
        if (model.ImageFiles is not null && model.ImageFiles.Any())
        {
            model.ImagePaths = new List<string>();

            foreach (var imageFile in model.ImageFiles)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = await _pictureService.UploadImage(imageFile!, "Requests", cancellationToken);
                    model.ImagePaths.Add(imagePath);
                }

            }


        }

        var requestDateTime = model.RequestForDate.Date + model.RequestForTime.TimeOfDay;

        var onlineUser = await _userManager.GetUserAsync(User);
        if (onlineUser == null)
        {
            ModelState.AddModelError("", "مشتری یافت نشد.");   //beporsim
            return View(model);
        }

        var homeService = await _homeServiceAppService.GetByIdAsync(model.HomeServiceId, cancellationToken);
        if (homeService == null)
        {
            ModelState.AddModelError("", "سرویس خانگی یافت نشد.");
            return View(model);
        }
        var cityId = onlineUser.CityId;

        var customer = await _customerAppService.GetCustomerInfoByIdAsync(onlineUser.Id, cancellationToken);
        if (customer == null)
        {
            ModelState.AddModelError("", "مشتری یافت نشد.");
            return View(model);
        }

        var newRequest = new Request
        {
            Title = model.HomeServiceTitle,
            RequestedDate = requestDateTime,
            RequestImages = model.ImagePaths,
            Description = model.Description,
            HomeServiceId = homeService.Id,
            CityId = cityId,
            CustomerId = customer.Id

        };

        var result = await _requestAppService.CreateAsync(newRequest, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "مشکلی در ایجاد درخواست رخ داده است.");
            return View(model);
        }

        return RedirectToAction("RequestList","Profile");


    }



    public async Task<IActionResult> RequestDetails(int id, CancellationToken cancellationToken)
    {
        var request = await _requestAppService.GetRequestByIdAsync(id, cancellationToken);
        if (request == null)
        {
            return NotFound();
        }
        return View(request);
    }
}
