using KhaneBan.Domain.Core.Contracts.AppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Admin.Controllers;

//[Authorize]
[Area("Admin")]
public class HomeController : Controller
{
    private readonly ICustomerAppService _customerAppService;

    public HomeController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
       var count= await _customerAppService.GetCountCustomerAsync(cancellationToken);
        return View(count);
    }
}
