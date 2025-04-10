using KhaneBan.Domain.Core.Contracts.AppService;
using KhaneBan.Domain.Core.Entites.UserRequests;
using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles ="Admin")]
public class SubCategoryController : Controller
{
    private readonly ISubCategoryAppService _subCategoryAppService;
    private readonly IPictureAppService _pictureAppService;
    private readonly ICategoryAppService _categoryAppService;
    private readonly ICategoryDapperAppService _categoryDapperAppService;
    private readonly ISubCategoryDapperAppService _subCategoryDapperAppService;

    public SubCategoryController(ICategoryAppService categoryAppService,
        ISubCategoryAppService subCategoryAppService,
        IPictureAppService pictureAppService,
        ICategoryDapperAppService categoryDapperAppService,
        ISubCategoryDapperAppService subCategoryDapperAppService)
    {
        _subCategoryAppService = subCategoryAppService;
        _pictureAppService = pictureAppService;
        _categoryAppService = categoryAppService;
        _categoryDapperAppService = categoryDapperAppService;
        _subCategoryDapperAppService = subCategoryDapperAppService;
    }

    public async Task<IActionResult> SubCategoryList(CancellationToken cancellationToken)
    {
        var categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);

        ViewData["categories"] = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Title
        }).ToList();

        var subCategories = await _subCategoryAppService.GetAllAsync(cancellationToken);

        return View(subCategories);


    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);

        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SubCategoryViewModel viewModel, CancellationToken cancellationToken)
    {

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);
            return View(viewModel);
        }

        if (viewModel.ImageFile is not null)
        {
            viewModel.ImagePath = await _pictureAppService.UploadImage(viewModel.ImageFile!, "SubCategories", cancellationToken);
        }

        var newSubCategory = new SubCategory
        {
            Title = viewModel.Title,
            CategoryId = viewModel.CategoryId,
            PicturePath = viewModel.ImagePath
        };

        var result = await _subCategoryAppService.CreateAsync(newSubCategory, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "ارور در منطق برنامه.");
            ViewBag.Categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);
            return View(viewModel);
        }

        return RedirectToAction("SubCategoryList");
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {

        var Flag = await _subCategoryAppService.DeleteAsync(id, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "زیر دسته بندی یافت نشد";
            return RedirectToAction("SubCategoryList");
        }

        TempData["DeleteResult"] = "زیر دسته بندی با موفقیت حذف گردید";
        return RedirectToAction("SubCategoryList");
    }

    public async Task<IActionResult> Active(int id, CancellationToken cancellationToken)
    {
        var Flag = await _subCategoryAppService.ActiveSubCategoryAsync(id, cancellationToken);
        if (!Flag)
        {
            TempData["CustomerNotFound"] = "زیر دسته بندی یافت نشد";
            return RedirectToAction("SubCategoryList");
        }

        TempData["DeleteResult"] = "زیر دسته بندی با موفقیت فعال گردید ";
        return RedirectToAction("SubCategoryList");
    }


    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var subCategory = await _subCategoryAppService.GetByIdAsync(id, cancellationToken);
        if (subCategory == null)
            return NotFound();
        var model = new EditSubCategoryViewModel
        {
            Id = subCategory.Id,
            Title = subCategory.Title,
            CategoryId = subCategory.CategoryId,
            ImagePath = subCategory.PicturePath
        };
        var categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);
        ViewBag.Categories = categories;



        return View(model);
    }



    [HttpPost]
    public async Task<IActionResult> Edit(EditSubCategoryViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);
            return View(model);
        }


        if (model.ImageFile is not null)
        {
            model.ImagePath = await _pictureAppService.UploadImage(model.ImageFile!, "SubCategories", cancellationToken);
        }

        var updatedSubCategory = new SubCategory
        {
            Id = model.Id,
            Title = model.Title,
            CategoryId = model.CategoryId,
            PicturePath = model.ImagePath
        };

        var result = await _subCategoryAppService.UpdateAsync(updatedSubCategory, cancellationToken);
        if (!result)
        {
            ModelState.AddModelError("", "ارور در منطقه برنامه.");
            ViewBag.Categories = await _categoryDapperAppService.GetAllAsync(cancellationToken);
            return View(model);
        }

        return RedirectToAction("SubCategoryList");
    }

}
