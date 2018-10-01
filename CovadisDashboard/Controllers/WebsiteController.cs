using CovadisDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CovadisDashboard.Controllers
{
    public class WebsiteController : Controller
    {
        // GET: /websites/
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"] = "I don't know what content will be displayed here, if any at all.";

            //Checks.WebsiteCheck check = new Checks.WebsiteCheck();
            //ViewData["data"] = check.RequestWebsites();

            return View();
        }
        
        // GET: /websites/{id}
        [HttpGet("/website/{id:int}")]
        public IActionResult Index(string id)
        {
            ViewData["id"] = id;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Message"] = "Here you can update an existing websites configuration.";

            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            ViewData["Message"] = "Here you can update an existing websites configuration.";

            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            ViewData["Message"] = "Here you can delete an existing websites configuration. Just be carefull it's not the wrong one!";

            return View();
        }

        [HttpPost]
        public IActionResult Add(int elements)
        {
            List<ElementModel> Elements = new List<ElementModel>();
            WebsiteModel Model = new WebsiteModel();

            //Model.Name = Request.Form["Name"];
            Model.Url = Request.Form["Url"];
            
            for(int i = 1; i <= elements; i++)
            {
                ElementModel elementModel = new ElementModel();
                string counter = i.ToString();
                string name = "Element" + i;
                elementModel.ElementName = Request.Form[name];
                Elements.Add(elementModel);
            }

            Model.Elements = Elements;

            string response = JsonConvert.SerializeObject(Model);

            return Content($"{response}");
        }
    }
}
