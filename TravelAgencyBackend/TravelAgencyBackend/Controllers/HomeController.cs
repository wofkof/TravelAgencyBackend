using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.Controllers
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
            ViewData["ActivePage"] = "Dashboard";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
