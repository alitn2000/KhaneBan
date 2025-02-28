using Microsoft.AspNetCore.Mvc;

namespace KhaneBan.EndPoints.MVC.Areas.Users;

[Area("Users")]
public class Profile : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult UpdateProfile()
    {
        return View();
    }
    public IActionResult SuggestionList()
    {
        return View();
    }

    public IActionResult RequestList()
    {
        return View();
    }
}
