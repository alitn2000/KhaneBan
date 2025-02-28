using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KhaneBan.EndPoints.MVC.Areas.Users.Controllers;

[Area("Users")]
public class HomeController : Controller
{
  
    public IActionResult Index()
    {

        return View();
    }
}
