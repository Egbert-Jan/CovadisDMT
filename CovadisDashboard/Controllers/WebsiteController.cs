using CovadisDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovadisDashboard.Controllers
{
    public class WebsiteController : Controller
    {
        // GETS //

        // GET: /websites/
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"] = "This is an overview of all the websites that are getting checked.";

            Checks.WebsiteCheck check = new Checks.WebsiteCheck();
            List<WebsiteModel> Websites = check.RequestWebsites("/api/websites");

            return View(Websites);
        }
        
        // GET: /website/{id}
        [HttpGet("/website/{id:int}")]
        public IActionResult Index(int id)
        {
            ViewData["id"] = id;

            //Checks.WebsiteCheck check = new Checks.WebsiteCheck();
            //ViewData["data"] = check.RequestWebsites("/websites/" + id);

            return View();
        }

        // GET: /website/add
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Message"] = "Here you can add a websites configuration.";

            return View();
        }

        // GET: /website/update/{id}
        [HttpGet("/website/update/{id:int}")]
        public IActionResult Update(int id)
        {
            ViewData["Message"] = "Here you can update an existing websites configuration.";
            ViewData["id"] = id;

            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            ViewData["Message"] = "Here you can delete an existing websites configuration. Just be carefull it's not the wrong one!";

            return View();
        }


        //POSTS//

        // POST: /website/add
        [HttpPost]
        public async Task<IActionResult> Add(int elements)
        {
            List<ElementModel> Elements = new List<ElementModel>();
            WebsiteModel Model = new WebsiteModel();

            //Model.Name = Request.Form["Name"];
            Model.url = Request.Form["Url"];
            
            for(int i = 1; i <= elements; i++)
            {
                ElementModel elementModel = new ElementModel();
                string counter = i.ToString();
                string name = "Element" + i;
                elementModel.elementName = Request.Form[name];
                Elements.Add(elementModel);
            }

            Model.elementen = Elements;

            var json = JsonConvert.SerializeObject(Model);

            var responseString = (String)null;

            try
            {
                var response = await Startup.client.PostAsJsonAsync("http://localhost:51226/api/websites", json);
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {

            }

            //Just for testing, remove later
            if(String.IsNullOrEmpty(responseString))
            {
                responseString = "Error submitting form data!";
            }

            return Content($"{responseString}");
        }
    }
}
