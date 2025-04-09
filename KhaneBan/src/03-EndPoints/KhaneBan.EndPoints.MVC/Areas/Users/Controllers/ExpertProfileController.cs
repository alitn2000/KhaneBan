using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.EndPoints.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;


[Area("Users")]
//[Authorize(Roles = "Expert")]
public class ExpertProfileController : Controller
{
    private readonly IExpertAppService _expertAppService;
    private readonly UserManager<User> _userManager;
    private readonly ICityDapperAppService _cityDapperAppService;
    private readonly IHomeServiceDapperAppService _homeServiceDapperAppService;
    private readonly IPictureAppService _pictureAppService;
    private readonly IRequestAppService _requestAppService;
    private readonly ISuggestionAppService _suggestionAppService;
    private readonly IHomeServiceAppService _homeServiceAppService;

    public ExpertProfileController(IExpertAppService expertAppService,
        UserManager<User> userManager,
        ICityDapperAppService cityDapperAppService,
        IHomeServiceDapperAppService homeServiceDapperAppService,
        IPictureAppService pictureAppService,
        IRequestAppService requestAppService,
        ISuggestionAppService suggestionAppService,
        IHomeServiceAppService homeServiceAppService)
    {
        _expertAppService = expertAppService;
        _userManager = userManager;
        _cityDapperAppService = cityDapperAppService;
        _homeServiceDapperAppService = homeServiceDapperAppService;
        _pictureAppService = pictureAppService;
        _requestAppService = requestAppService;
        _suggestionAppService = suggestionAppService;
        _homeServiceAppService = homeServiceAppService;
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

    public async Task<IActionResult> UpdateExpertProfile(CancellationToken cancellationToken)
    {
        var cities = await _cityDapperAppService.GetAllAsync(cancellationToken);
        ViewBag.Cities = cities.Select(sc => new SelectListItem
        {
            Value = sc.Id.ToString(),
            Text = sc.Title
        }).ToList();

        var homeServices = await _homeServiceDapperAppService.GetAllAsync(cancellationToken);
        ViewBag.HomeServices = homeServices.Select(sh => new SelectListItem
        {
            Value = sh.Id.ToString(),
            Text = sh.Title
        }).ToList();



        var onlineUser = await _userManager.GetUserAsync(User);                                                                


        if (onlineUser is null)
            return RedirectToAction("Login", "Account");

        var model = new UpdateExpertViewModel
        {
            Id = onlineUser.Id,
            FirstName = onlineUser.FirstName,
            LastName = onlineUser.LastName,
            Email = onlineUser.Email,
            Address = onlineUser.Address,
            ImagePath = onlineUser.PicturePath,
            PhoneNumber = onlineUser.PhoneNumber,
            CityId = onlineUser.CityId,
            UserName = onlineUser.UserName,
            Price = onlineUser.Balance,
            Skills = onlineUser.Expert?.HomeServices?.Select(hs => hs.Id).ToList() ?? new List<int>()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateExpertProfile(UpdateExpertViewModel model, List<int> SelectedHomeServices, CancellationToken cancellationToken)
    {
        var cities = await _cityDapperAppService.GetAllAsync(cancellationToken);
        var homeServices = await _homeServiceDapperAppService.GetAllAsync(cancellationToken);

        if (!ModelState.IsValid)
        {
            ViewBag.Cities = cities.Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.Title
            }).ToList();

            ViewBag.HomeServices = homeServices.Select(sh => new SelectListItem
            {
                Value = sh.Id.ToString(),
                Text = sh.Title
            }).ToList();


            return View(model);
        }

        if (model.ImageFile is not null)
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "Experts", cancellationToken);
        }

        var onlineUser = await _userManager.GetUserAsync(User);
        if (onlineUser is null)
            return RedirectToAction("Login", "Account");



        if (model.ImageFile is null)
        {
            model.ImagePath = onlineUser.PicturePath;
        }
        else
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "Experts", cancellationToken);
        }

        onlineUser.Id = model.Id;
        onlineUser.FirstName = model.FirstName;
        onlineUser.LastName = model.LastName;
        onlineUser.Email = model.Email;
        onlineUser.Address = model.Address;
        onlineUser.PicturePath = model.ImagePath;
        onlineUser.PhoneNumber = model.PhoneNumber;
        onlineUser.CityId = model.CityId;
        onlineUser.UserName = model.UserName;
        onlineUser.Balance = model.Price;


        var expert = await _expertAppService.GetByIdAsync(onlineUser.Id, cancellationToken);

        var result = await _expertAppService.UpdateAsync(expert, model.Skills, cancellationToken);

        if (result)
        {
            ViewBag.UpdateMessage = "information updated successfully";
            return RedirectToAction("UpdateExpertProfile");
        }
        ViewBag.UpdateMessage = "information did not updated ";

        return RedirectToAction("UpdateExpertProfile");
    }

    public async Task<IActionResult> SendSuggestion(int requestId, CancellationToken cancellationToken)
    {
        var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (onlineUserId is null)
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(onlineUserId);
        var expert = await _expertAppService.GetExpertProfileByIdAsync(userId, cancellationToken);

        if (expert == null)
        {
            return NotFound("Expert not found.");
        }

        var request = await _requestAppService.GetRequestByIdAsync(requestId, cancellationToken);

        if (request == null)
        {
            return NotFound("Request not found.");
        }



        var model = new AddSuggestionViewModel
        {
            RequestId = request.Id,
            OfferDate = DateTime.Now,
            ExpertId = expert.Id
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SendSuggestion(AddSuggestionViewModel model, CancellationToken cancellationToken)
    {
        var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (onlineUserId is null)
            return RedirectToAction("Login", "Account");

        int expertId = int.Parse(onlineUserId);
        double servicePrice = await _homeServiceAppService.GetBasePriceByRequestId(model.RequestId, cancellationToken);
        if (model.SuggestedPrice < servicePrice)
        {
            ViewBag.PriceError = "قیمت پیشنهادی پایین تر از قیمت پایه است";
            return View(model);
        }
        var newOffer = new Suggestion
        {
            ExpertId = model.ExpertId,
            RequestId = model.RequestId,
            Price = model.SuggestedPrice,
            Description = model.Description,
            StartDate = model.OfferDate
        };

        var result = await _suggestionAppService.CreateAsync(newOffer, cancellationToken);

        if (!result)
        {
            ModelState.AddModelError("", "خطا رخ داده است.");
            return View(model);
        }
        var requestResult = await _requestAppService.UpdateStatusAsync(newOffer.RequestId,StatusEnum.WaitingCustomerToChoose,cancellationToken);
        if (!requestResult.Flag)
        {
            ModelState.AddModelError("", "خطا رخ داده است.");
            return View(model);
        }
        return RedirectToAction("ShowRequests","ExpertProfile");

    }

    public async Task<IActionResult> RequestDetails(int id, CancellationToken cancellationToken)
    { 
        var request = await _requestAppService.GetRequestByIdAsync(id, cancellationToken);
        return View(request);
    }

    public async Task<IActionResult> ShowRequests(CancellationToken cancellationToken)
    {
        var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (onlineUserId is null)
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(onlineUserId);
        var expert = await _expertAppService.GetExpertByIdWithDetailsAsync(userId, cancellationToken);
        if (expert?.HomeServices == null)
        {
            return RedirectToAction("Index");
        }

        var expertHomeServices = expert.HomeServices.Select(x => x.Id).ToList();
        var requests = await _requestAppService.GetRequestsByHomeServices(expertHomeServices, expert.User.CityId, cancellationToken);

        return View(requests);

    }


}
