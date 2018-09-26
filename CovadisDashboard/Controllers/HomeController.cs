﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CovadisDashboard.Models;

namespace CovadisDashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNew()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult EditWebsite()
        {
            ViewData["Message"] = "Your contact page.";

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

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddNew(WebsiteModel model)
        {
            if (string.IsNullOrEmpty(model.Url))
            {
                return Content($"No url was specified, please try again.");
            }
            else
            {
                string baseString = $"This is your url: {model.Url}, " +
                    $"the 1st element: {model.Element1}";
                if (!string.IsNullOrEmpty(model.Element2))
                {
                    baseString += $"the 2nd element: {model.Element2} ";
                }

                if (!string.IsNullOrEmpty(model.Element3))
                {
                    baseString += $"the 3rd element: {model.Element3} ";
                }

                return Content(baseString);
            }

        }
    }
}
