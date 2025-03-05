using KhaneBan.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;

[Area("Users")]
public class HomeServiceController : Controller
{
    private readonly IHomeServiceAppService _homeServiceAppService;

    public HomeServiceController(IHomeServiceAppService homeServiceAppService)
    {
        _homeServiceAppService = homeServiceAppService;
    }
    public async Task<IActionResult> HomeServiceList(int subCategoryId, CancellationToken cancellationToken)
    {

        var homeServices = await _homeServiceAppService.GetHomeServicesBySubCategoryId(subCategoryId, cancellationToken);

        if (homeServices == null || !homeServices.Any())
        {
            return NotFound();
        }

        return View(homeServices);

    }
}