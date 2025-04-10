using KhaneBan.Domain.AppServices;
using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class CategoryController : Controller
{

    private readonly ICategoryAppService _categoryAppService;
    private readonly IPictureAppService _pictureAppService ;
    private readonly ICategoryDapperAppService _categoryDapperAppService;

    public CategoryController(ICategoryAppService categoryAppService,
        IPictureAppService pictureAppService,
        ICategoryDapperAppService categoryDapperAppService)
    {
        _categoryDapperAppService = categoryDapperAppService;
        _categoryAppService = categoryAppService;
        _pictureAppService = pictureAppService;
    }

    public async Task<IActionResult> CategoryList(CancellationToken cancellationToken)
    {
        var categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryViewModel model, CancellationToken cancellationToken)
    {

        if (!ModelState.IsValid)
            return View(model);


        if (model.ImageFile is not null)
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "Categories", cancellationToken);
        }

        var newCategory = new Category
        {
            Title = model.Title,
            PicturePath = model.ImagePath
        };

        var result = await _categoryAppService.CreateAsync(newCategory, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "ارور در سطح منطقی");
            return View(model);
        }

        return RedirectToAction("CategoryList");
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {

        var Flag = await _categoryAppService.DeleteAsync(id, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "کتگوری یافت نشد";
            return RedirectToAction("CategoryList");
        }

        TempData["DeleteResult"] = "کتگوری با موفقیت حذف گردید";
        return RedirectToAction("CategoryList");
    }

    public async Task<IActionResult> Active(int id, CancellationToken cancellationToken)
    {
        var Flag = await _categoryAppService.ActiveCategoryAsync(id, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "کتگوری یافت نشد";
            return RedirectToAction("CategoryList");
        }

        TempData["DeleteResult"] = "کتگوری با موفقیت فعال گردید ";
        return RedirectToAction("CategoryList");
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var category = await _categoryAppService.GetByIdAsync(id, cancellationToken);
        if (category == null)
            return NotFound();

        var model = new UpdateCategoryViewModel
        {
            Id = category.Id,
            Title = category.Title,
            ImagePath = category.PicturePath
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateCategoryViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (model.ImageFile is not null)
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "Categories", cancellationToken);
        }

        var updatedCategory = new Category
        {
            Id = model.Id,
            Title = model.Title,
            PicturePath = model.ImagePath
        };


        var result = await _categoryAppService.UpdateAsync(updatedCategory, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "خطایی رخ داده است");
            return View(model);
        }

        return RedirectToAction("CategoryList");
    }

}
