using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Enums;
using KhaneBan.EndPoints.MVC.Areas.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KhaneBan.EndPoints.MVC.Areas.Users;

[Area("Users")]
[Authorize(Roles ="Customer")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ICustomerAppService _customerAppService;
    private readonly ICityAppService _cityAppService;
    private readonly IPictureAppService _pictureAppService;
    private readonly IRequestAppService _requestAppService;
    private readonly ISuggestionAppService _suggestionAppService;
    private readonly IRatingAppService _ratingAppService;


    public ProfileController(UserManager<User> userManager,
        ICustomerAppService customerAppService,
        ICityAppService cityAppService,
        IPictureAppService pictureAppService,
        IRequestAppService requestAppService,
        ISuggestionAppService suggestionAppService,
        IRatingAppService ratingAppService)
    {
        _userManager = userManager;
        _customerAppService = customerAppService;
        _cityAppService = cityAppService;
        _pictureAppService = pictureAppService;
        _requestAppService = requestAppService;
        _suggestionAppService = suggestionAppService;
        _ratingAppService = ratingAppService;
    }


    public async Task<IActionResult> Index( CancellationToken cancellationToken)
    {
        var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);                    


        if (onlineUserId is null)
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(onlineUserId);

        var userInfo = await _customerAppService.GetCustomerByIdWithDetailsAsync(userId, cancellationToken);


        return View(userInfo);


    }


    public async Task<IActionResult> UpdateCustomerProfile(CancellationToken cancellationToken)
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
            UserName = onlineUser.UserName, 
            Balance = onlineUser.Balance
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


        if (model.ImageFile is null)
        {
            model.ImagePath = onlineUser.PicturePath;
        }
        else
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "Customers", cancellationToken);
        }
        onlineUser.FirstName = model.FirstName;
        onlineUser.LastName = model.LastName;
        onlineUser.Email = model.Email;
        onlineUser.Address = model.Address;
        onlineUser.PicturePath = model.ImagePath;
        onlineUser.PhoneNumber = model.PhoneNumber;
        onlineUser.CityId = model.CityId;
        onlineUser.UserName = model.UserName;
        onlineUser.Balance = model.Balance;
        ///////password ro ham update konim
       var result = await _customerAppService.UpdateAsync(onlineUser);

        if (result.Succeeded)
        {
            ViewBag.UpdateMessage = "ویرایش اطلاعات با موفقیت انجام شد";
            return RedirectToAction("Index");
        }
        ViewBag.UpdateMessage = "ویرایش اطلاعات انجام نشد";

        return RedirectToAction("UpdateCustomerProfile");
    }



    public async Task<IActionResult> SuggestionList(int id, CancellationToken cancellationToken)
    {
        var suggestions = await _suggestionAppService.GetRequestSuggestions(id, cancellationToken);
        return View(suggestions);
    }

    public async Task<IActionResult> SuggestionsDetail(int id, CancellationToken cancellationToken)
    {
        var suggestion = await _suggestionAppService.GetByIdAsync(id, cancellationToken);
        if (suggestion == null)
        {
            return NotFound();
        }
        return View(suggestion);
    }
    public IActionResult EditCustomerInfo()
    {
        return View();
    }

    public async Task<IActionResult> RequestList(CancellationToken cancellationToken)
    {
        var onlineUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (onlineUserId is null)
            return RedirectToAction("Login", "Account");

        int userId = int.Parse(onlineUserId);

        var requests = await _requestAppService.GetCustomersRequestAsync(userId, cancellationToken); 

        return View(requests);
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

    [HttpPost]
    public async Task<IActionResult> ConfirmSuggestion(int id, CancellationToken cancellationToken)
    {
        

        var suggestion = await _suggestionAppService.GetByIdAsync(id, cancellationToken);

        if (suggestion == null)
        {
            return NotFound();
        }

       var suggestionResult = await _suggestionAppService.UpdateStatusAsync(suggestion.Id, StatusEnum.WorkStarted, cancellationToken);

        if (suggestionResult.Flag)
        {

            var requestResult = await _requestAppService.UpdateStatusAsync(suggestion.RequestId, StatusEnum.WorkStarted, cancellationToken);
            if (requestResult.Flag)
            {
                var setWinnerResult = await _requestAppService.SetWinner(suggestion.RequestId, suggestion.Id, cancellationToken);
                TempData["ResultMessage"] = setWinnerResult.Message;
                return RedirectToAction("RequestList");
            }

            TempData["ResultMessage"] = requestResult.Message;

           
            TempData["ResultMessage"] = suggestionResult.Message;
            return RedirectToAction("RequestList");

        }

        TempData["ResultMessage"] = suggestionResult.Message;
        return RedirectToAction("RequestList");
    }

    public  async Task<IActionResult> Payment(int id, CancellationToken cancellationToken)
    {
        var onlineUser = await _userManager.GetUserAsync(User);
        var winner = await _suggestionAppService.GetByIdAsync(id, cancellationToken);
        if (winner == null)
            return NotFound();
        ViewBag.Price = winner.Price;
        ViewBag.SuggestionId = winner.Id;
        ViewBag.RequestId = winner.RequestId;


        return View(onlineUser);
    }
    [HttpPost]
    public async Task<IActionResult> Payment(int SuggestionId, int RequestId, double minPrice,string selectedAmount, CancellationToken cancellationToken)
    {
        var onlineUser = await _userManager.GetUserAsync(User);
        if (onlineUser == null)
            return NotFound();

        double price = 0;

      
        if(!string.IsNullOrEmpty(selectedAmount) && double.TryParse(selectedAmount, out double defaultMoney))
        {
            price = defaultMoney;
        }
        else
        {
            ModelState.AddModelError("", "لطفاً مبلغ معتبری وارد کنید.");
            return View(onlineUser);
        }

        var result = await _customerAppService.MinusBalanceAsync(onlineUser.Id, price, cancellationToken);
        if (result.Flag)
        {
            var suggestionResult = await _suggestionAppService.UpdateStatusAsync(SuggestionId, StatusEnum.WorkPaidByCustomer, cancellationToken);
            if (suggestionResult.Flag)
            {

                var requestResult = await _requestAppService.UpdateStatusAsync(RequestId, StatusEnum.WorkPaidByCustomer, cancellationToken);
                if (requestResult.Flag)
                {

                    TempData["ResultMessage"] = "پرداخت وجه موفقیت امیز بود";
                    return RedirectToAction("RequestList");
                }

                TempData["ResultMessage"] = "خطا در پرداخت وجه: تغییر وضعیت درخواست صورت نگرفت";
                return RedirectToAction("RequestList");

            }
        }
        TempData["ResultMessage"] = "خطا در پرداخت وجه: تغییر وضعیت پیشنهاد صورت نگرفت";

        return RedirectToAction("RequestList");
    }

    public async Task<IActionResult> SetRating(int requestId, int winnerId, CancellationToken cancellationToken)
    {
        var onlineUser = await _userManager.GetUserAsync(User);

        if (onlineUser is null)
            return RedirectToAction("Login", "Account");
        var expertId = await _requestAppService.GetWinnerExpertIdAsync(requestId, cancellationToken);

        var model = new CreateRatingDTO
        {
            RequestId = requestId,
            ExpertId = expertId ?? 0
        };

        ViewBag.UserName = $"{onlineUser.FirstName} {onlineUser.LastName}";

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SetRating(CreateRatingDTO model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var onlineUser = await _userManager.GetUserAsync(User);

        if (onlineUser is null)
            return RedirectToAction("Login", "Account");

        var customer = await _customerAppService.GetCustomerInfoByIdAsync(onlineUser.Id, cancellationToken);

        if (customer == null)
        {

            ModelState.AddModelError(string.Empty, "مشتری یافت نشد.");
            return View(model);
        }



        model.CustomerId = customer.Id;

        await _ratingAppService.CreateAsync(model, cancellationToken);

        var request = await _requestAppService.GetRequestByIdAsync(model.RequestId, cancellationToken);
        if (request != null)
        {
            request.IsReviewd = true;
            await _requestAppService.UpdateAsync(request, cancellationToken);
        }
        TempData["ResultMessage"] = "your comment succefully registerd.";
        return RedirectToAction("RequestList");
    }

}


