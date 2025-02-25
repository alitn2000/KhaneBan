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
//[Authorize(Roles ="Admin")]
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
    public AdminController(UserManager<User> userManager,
        IAdminAccountAppService adminAccountAppService,
        SignInManager<User> signInManager,
        ICustomerAppService customerAppService,
        IExpertAppService expertAppService,
        ICityAppService cityAppService,
        IRequestAppService requestAppService,
        ISuggestionAppService suggestionAppService,
        IRatingAppService ratingAppService)
        
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

        var user = await _userManager.FindByNameAsync(adminViewModel.UserName);
        if (user == null)
        {
            TempData["LoginResult"] = "شناسه کاربری یا رمز عبور اشتباه است";
            return View(adminViewModel);
        }
        var result = await _adminAccountAppService.LoginAsync(adminViewModel.UserName, adminViewModel.Password);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        TempData["LoginResult"] = "شناسه کاربری یا رمز عبور اشتباه است";
        return View(adminViewModel);
    }

    [HttpGet]

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Admin", new { area = "Admin" });
    }
    #endregion

    #region Customer
    //----------Customer----------//
    public async Task<IActionResult> CustomerList(CancellationToken cancellationToken)
    {
        var customers = await _customerAppService.GetCustomerInfoAsync();


        List<UserViewModel> users = new List<UserViewModel>();

        foreach (var customer in customers)
        {
            string picturePath = customer.User.PicturePath;

            if (!string.IsNullOrEmpty(picturePath) && !picturePath.StartsWith("http") && !picturePath.Contains("/"))
            {
                picturePath = $"data:image/jpeg;base64,{picturePath}";
            }
            users.Add(new UserViewModel()
            {
                Id = customer.Id,
                Email = customer.User.Email,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                Adress = customer.User.Address,
                PicturePath = picturePath,
                PhoneNumber = customer.User.PhoneNumber,
                IsDeleted = customer.User.IsDeleted,

            });
        }
        return View(users);
    }


    public async Task<IActionResult> DeleteCustomer(int userId, CancellationToken cancellationToken)
    {

        var Flag = await _customerAppService.DeleteAsync(userId, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "مشتری یافت نشد";
            return RedirectToAction("CustomerList");
        }

        TempData["DeleteResult"] = "مشتری با موفقیت حذف گردید";
        return RedirectToAction("CustomerList");
    }

    public async Task<IActionResult> ActiveCustomer(int userId, CancellationToken cancellationToken)
    {
        var Flag = await _customerAppService.ActiveCustomer(userId, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "مشتری یافت نشد";
            return RedirectToAction("CustomerList");
        }

        TempData["DeleteResult"] = "مشتری با موفقیت فعال گردید ";
        return RedirectToAction("CustomerList");
    }

    [HttpGet]
    public async Task<IActionResult> CreateCustomer(CancellationToken cancellationToken)
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
    public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel viewModel, CancellationToken cancellationToken)
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

        string picture = string.Empty;
        if (viewModel.ProfileImage != null && viewModel.ProfileImage.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                await viewModel.ProfileImage.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);

                string fileType = viewModel.ProfileImage.ContentType;
                picture = $"data:{fileType};base64,{base64String}";
            }
        }


        var user = new User
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            Address = viewModel.Address,
            UserName = viewModel.UserName,
            CityId = viewModel.CityId,
            PicturePath = picture
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);
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
        var customer = new Customer
        {
            UserId = user.Id,
            User = user
        };

        var flag = await _customerAppService.CreateAsync(customer, cancellationToken);
        if (flag)
        {
            TempData["Message"] = "مشتری با موفقیت اضافه شد";    /////////////////بولیناش بررسی شود 
            ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList());
            return RedirectToAction("CustomerList");
        }
        TempData["Message"] = "مشتری موجود است ";

        ViewData["Cities"] = JsonConvert.SerializeObject(cities.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Title
        }).ToList());
        return View(viewModel);
    }


    

    public async Task<IActionResult> UpdateCustomer(int userId, CancellationToken cancellationToken)
    {
        var customer = await _customerAppService.GetCustomerInfoByIdAsync(userId, cancellationToken);
        if (customer == null)
           return NotFound();         /////////// taghir bede

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
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return NotFound();             ////// taghir bede

        user.UserName = model.Email;
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        var customer = await _customerAppService.GetCustomerInfoByIdAsync(model.Id, cancellationToken);
        if (customer == null)
            return NotFound();

        customer.User = user;
        await _customerAppService.UpdateAsync(customer, cancellationToken);
        return RedirectToAction("CustomerList", "Admin");

    }
    #endregion

    #region Expert
    //----------Expert----------//

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
            TempData["CustomerNotFound"] = "کارشناس یافت نشد";
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

        var user = new User
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            Address = viewModel.Address,
            UserName = viewModel.UserName,
            CityId = viewModel.CityId,
        };

        var result = await _userManager.CreateAsync(user, viewModel.Password);
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
            return RedirectToAction("CustomerList");
        }

        TempData["DeleteResult"] = "مشتری با موفقیت فعال گردید ";
        return RedirectToAction("ExpertList");
    }


    public async Task<IActionResult> UpdateExpert(int userId, CancellationToken cancellationToken)
    {
        var customer = await _expertAppService.GetExpertInfoByIdAsync(userId, cancellationToken);
        if (customer == null)
            return NotFound();         /////////// taghir bede

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
            return NotFound();             ////// taghir bede

        user.UserName = model.Email;
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        return RedirectToAction("ExpertList", "Admin");

    }

    #endregion

    #region Request

    //----------Request----------//
    public async  Task<IActionResult> RequestList(CancellationToken cancellationToken)
    {
        var requests = await _requestAppService.GetRequestsInfo(cancellationToken);

        return View(requests);
    }

    public async Task<IActionResult> RequestsSuggestions(int requestId, CancellationToken cancellationToken)
    {
      var suggestions = await _suggestionAppService.GetRequestSuggestions(requestId, cancellationToken);

        return View(suggestions);
    }

    public async Task<IActionResult> ChangeStatus(int requestId,StatusEnum Statusenum, CancellationToken cancellationToken)
    {
        bool flag = await _requestAppService.ChangeStatus(requestId, Statusenum, cancellationToken);

        if (flag)
        {
            ViewBag.StatusMessage = "وضعیت سفارش با موفقیت تغییر یافت";
            return RedirectToAction("RequestList");
        }

        ViewBag.StatusMessageFaild = "تغییر وضعیت صورت نگرفت";
        return RedirectToAction("RequestList");

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
                TempData["UpdateStatus"] = "تغییر وضعیت با موفقییت ثبت شد";
                return RedirectToAction("RatingList");
            }

            TempData["UpdateStatus"] = "نظر مشتری یافت نشد";
            return RedirectToAction("RatingList");
        }

        else
        {
            if (await _ratingAppService.Reject(ratingId, cancellationToken))
            {
                TempData["UpdateStatus"] = "نظر مشتری رد صلاحیت شد و حذف شد";
                return RedirectToAction("RatingList");
            }



            TempData["UpdateStatus"] = "نظر مشتری یافت نشد";
            return RedirectToAction("RatingList");
        }
        
    }
    #endregion

}
