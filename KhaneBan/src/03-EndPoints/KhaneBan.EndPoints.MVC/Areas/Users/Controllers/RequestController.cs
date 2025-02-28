using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;

public class RequestController : Controller
{
    [Area("Users")]
    public IActionResult AddRequest()
    {
        return View();
    }
}
