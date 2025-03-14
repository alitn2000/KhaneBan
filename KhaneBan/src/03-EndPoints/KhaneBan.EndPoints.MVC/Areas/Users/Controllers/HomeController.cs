using KhaneBan.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;

[Area("Users")]
public class HomeController : Controller
{

    private readonly ICategoryAppService _categoryAppService;
    private readonly ICategoryDapperAppService _categoryDapperAppService;
    public HomeController(ICategoryAppService categoryAppService, ICategoryDapperAppService categoryDapperAppService)
    {
        _categoryAppService = categoryAppService;
        _categoryDapperAppService = categoryDapperAppService;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var categories = await _categoryAppService.GetAllWithDetailsAsync(cancellationToken);
        return View(categories);
    }
}


