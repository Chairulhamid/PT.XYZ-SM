﻿using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Project()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            return View();
        }
        public IActionResult LogiinPage()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Vendor()
        {
            return View();
        }
        [Authorize]
        public IActionResult VendorManager()
        {
            return View();
        }
        [Authorize]
        public IActionResult AllProject()
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
