using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Enums;
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
    private readonly IRequestAppService _requestAppService;
    private readonly ISuggestionAppService _suggestionAppService;

    public ProfileController(UserManager<User> userManager,
        ICustomerAppService customerAppService,
        ICityAppService cityAppService,
        IPictureAppService pictureAppService,
        IRequestAppService requestAppService,
        ISuggestionAppService suggestionAppService)
    {
        _userManager = userManager;
        _customerAppService = customerAppService;
        _cityAppService = cityAppService;
        _pictureAppService = pictureAppService;
        _requestAppService = requestAppService;
        _suggestionAppService = suggestionAppService;
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
    public async Task<IActionResult> Payment(int SuggestionId, int RequestId, double minPrice,string selectedAmount, string customAmount, CancellationToken cancellationToken)
    {
        var onlineUser = await _userManager.GetUserAsync(User);
        if (onlineUser == null)
            return NotFound();

        double price = 0;

        if (!string.IsNullOrEmpty(customAmount) && double.TryParse(customAmount, out double customMoney) && customMoney > 0)
        {
            if (customMoney < minPrice)
            {
                ModelState.AddModelError("customAmount", $"حداقل مبلغ پرداختی {minPrice} تومان است.");
                return View(onlineUser); 
            }
            price = customMoney;
        }
        else if (!string.IsNullOrEmpty(selectedAmount) && double.TryParse(selectedAmount, out double defaultMoney))
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
        TempData["PaymentResult"] = "خطا در پرداخت وجه: تغییر وضعیت پیشنهاد صورت نگرفت";

        return RedirectToAction("RequestList");
    }
}


