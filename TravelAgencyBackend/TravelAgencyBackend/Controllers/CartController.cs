﻿using Microsoft.AspNetCore.Mvc;

namespace TravelAgencyBackend.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
