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
public class CustomerController : Controller
{
    private readonly ICustomerAppService _customerAppService;
    private readonly ICityAppService _cityAppService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IPictureAppService _pictureAppService;


    public CustomerController(ICustomerAppService customerAppService,
        ICityAppService cityAppService,
        UserManager<User> userManager,
        SignInManager<User> signInManager
        , IPictureAppService pictureAppService)
    {
        _customerAppService = customerAppService;
        _cityAppService = cityAppService;
        _userManager = userManager;
        _signInManager = signInManager;
        _pictureAppService = pictureAppService;
    }

    public async Task<IActionResult> CustomerList(CancellationToken cancellationToken)
    {
        var customers = await _customerAppService.GetCustomerInfoAsync();


        List<UserViewModel> users = new List<UserViewModel>();

        foreach (var customer in customers)
        {
            

           
            users.Add(new UserViewModel()
            {
                Id = customer.User.Id,
                Email = customer.User.Email,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                Adress = customer.User.Address,
                PicturePath = customer.User.PicturePath,
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

        if (viewModel.ProfileImgFile is not null)
        {
            viewModel.ImagePath = await _pictureAppService.UploadImage(viewModel.ProfileImgFile!, "Customers", cancellationToken);
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

        var result = await _customerAppService.RegisterAsync(user, viewModel.Password);
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
        await _userManager.AddToRoleAsync(user, "Customer");
        var customer = new Customer
        {
            UserId = user.Id,
            User = user
        };

        var flag = await _customerAppService.CreateAsync(customer, cancellationToken);


        if (flag)
        {
            TempData["Message"] = "مشتری با موفقیت اضافه شد";    
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
            return NotFound();             

        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;

        var result = await _customerAppService.UpdateAsync(user);
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
        return RedirectToAction("CustomerList");

    }
}
