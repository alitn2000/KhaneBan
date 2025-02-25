using KhaneBan.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KhaneBan.EndPoints.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("About page visited at {Time} by User {UserId}", DateTime.UtcNow.ToLongTimeString(), 5);
           // _logger.LogInformation("About page visited at" + DateTime.UtcNow.ToLongTimeString());

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
