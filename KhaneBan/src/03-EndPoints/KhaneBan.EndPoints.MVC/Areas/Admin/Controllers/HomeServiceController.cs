using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class HomeServiceController : Controller
{
    private readonly IHomeServiceAppService _homeServiceAppService;
    private readonly ISubCategoryAppService _subCategoryAppService;
    private readonly IPictureAppService _PictureAppService;



    public HomeServiceController(IHomeServiceAppService homeServiceAppService, ISubCategoryAppService subCategoryAppService, IPictureAppService PictureAppService)
    {
        _homeServiceAppService = homeServiceAppService;
        _subCategoryAppService = subCategoryAppService;
        _PictureAppService = PictureAppService;


    }

    public async Task<IActionResult> HomeServiceList(CancellationToken cancellationToken)
    {
        var homeServices = await _homeServiceAppService.GetAllAsync(cancellationToken);
        return View(homeServices);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
        ViewBag.SubCategories = subCategories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Title
        }).ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHomeServiceViewModel model, CancellationToken cancellationToken)
    {

        if (!ModelState.IsValid)
        {
            ViewBag.SubCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
            return View(model);
        }
        if (model.ImageFile is not null)
        {
            model.ImagePath = await _PictureAppService.UploadImage(model.ImageFile!, "HomeServices", cancellationToken);
        }
        var newHomeService = new HomeService
        {
            Title = model.Title,
            SubCategoryId = model.SubCategoryId,
            PicturePath = model.ImagePath,
            BasePrice = model.Price,
        };

        var result = await _homeServiceAppService.CreateAsync(newHomeService, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "ارور در منطق برنامه.");
            ViewBag.SubCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
            return View(model);
        }

        return RedirectToAction("HomeServiceList");
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var homeService = await _homeServiceAppService.GetByIdAsync(id, cancellationToken);
        if (homeService == null)
            return NotFound();

        var subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
        ViewBag.SubCategories = subCategories.Select(sc => new SelectListItem
        {
            Value = sc.Id.ToString(),
            Text = sc.Title
        }).ToList();

        var model = new EditHomeServiceViewModel
        {
            Id = homeService.Id,
            Title = homeService.Title,
            SubCategoryId = homeService.SubCategoryId,
            ImagePath = homeService.PicturePath,
            Price = homeService.BasePrice,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditHomeServiceViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Subcategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
            return View(model);
        }

        if (model.ImageFile is not null)
        {
            model.ImagePath = await _PictureAppService.UploadImage(model.ImageFile!, "HomeServices", cancellationToken);
        }
        var updatedHomeService = new HomeService
        {
            Id = model.Id,
            Title = model.Title,
            SubCategoryId = model.SubCategoryId,
            PicturePath = model.ImagePath,
            BasePrice = model.Price,

        };

        var result = await _homeServiceAppService.UpdateAsync(updatedHomeService, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "ارور در منطق برنامه.");
            ViewBag.subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);
            return View(model);
        }

        return RedirectToAction("HomeServiceList");
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {

        var Flag = await _homeServiceAppService.DeleteAsync(id, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "هوم سرویس یافت نشد";
            return RedirectToAction("HomeServiceList");
        }

        TempData["DeleteResult"] = "هوم سرویس با موفقیت حذف گردید";
        return RedirectToAction("HomeServiceList");
    }

    public async Task<IActionResult> Active(int id, CancellationToken cancellationToken)
    {
        var Flag = await _homeServiceAppService.ActiveHomeServiceAsync(id, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "هوم سرویس یافت نشد";
            return RedirectToAction("HomeServiceList");
        }

        TempData["DeleteResult"] = "هوم سرویس با موفقیت فعال گردید ";
        return RedirectToAction("HomeServiceList");
    }

}
