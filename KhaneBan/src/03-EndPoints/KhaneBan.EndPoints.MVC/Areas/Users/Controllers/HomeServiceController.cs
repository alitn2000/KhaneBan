using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;

[Area("Users")]
public class HomeServiceController : Controller
{
    public IActionResult HomeServiceList()
    {
        return View();
    }
}
