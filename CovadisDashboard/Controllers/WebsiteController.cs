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

        // GET: /websites
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"] = "This is an overview of all the websites that are getting checked.";

            Checks.WebsitesCheck check = new Checks.WebsitesCheck();
            List<WebsiteModel> Websites = check.RequestWebsites("/websites");

            return View(Websites);
        }
        
        // GET: /website/details/{id}
        [HttpGet("/website/details/{id:int}")]
        public IActionResult Details(int id)
        {
            ViewData["id"] = id;

            Checks.WebsiteCheck check = new Checks.WebsiteCheck();
            WebsiteModel Model = check.RequestWebsite("/websites/" + id);

            return View(Model);
        }

        // GET: /website/add
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "Here you can add a websites configuration.";

            return View();
        }

        // GET: /website/update/{id}
        [HttpGet]
        [Route("/website/update/{id:int}")]
        public IActionResult Edit(int id)
        {
            ViewData["Message"] = "Here you can update an existing websites configuration.";
            ViewData["id"] = id;

            Checks.WebsiteCheck check = new Checks.WebsiteCheck();

            WebsiteModel Model = check.RequestWebsite("/websites/" + id);
            
            return View(Model);
        }
        

        // POSTS //

        // POST: /website/add
        [HttpPost]
        public async Task<IActionResult> Create(int elements)
        {
            List<ElementModel> Elements = new List<ElementModel>();
            WebsiteModel Model = new WebsiteModel();

            Model.Name = Request.Form["Name"];
            Model.Url = Request.Form["Url"];
            
            for(int i = 1; i <= elements; i++)
            {
                ElementModel elementModel = new ElementModel();
                string counter = i.ToString();
                string name = "Element" + i;
                elementModel.Name = Request.Form[name];
                Elements.Add(elementModel);
            }

            Model.Elements = Elements;

            var responseString = (String)null;

            try
            {
                var response = await Startup.client.PostAsJsonAsync("http://localhost:51226/api/websites", Model);
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {

            }

            //return Content($"{responseString}");
            return Redirect("/website");
        }


        // PUTS //
        // PUT: /website/update/{id}
        [HttpPost("/website/update/{id:int}")]
        public async Task<IActionResult> Edit(WebsiteModel Model, int elements, int id)
        {
            List<ElementModel> Elements = new List<ElementModel>();
            
            Model.Name = Request.Form["Name"];
            Model.Url = Request.Form["Url"];

            for (int i = 1; i <= elements; i++)
            {
                ElementModel elementModel = new ElementModel();
                string counter = i.ToString();
                string name = "Element" + i;
                string elementId = "Element" + i + "Id";
                elementModel.Name = Request.Form[name];
                elementId = Request.Form[name + "Id"];
                elementModel.Id = int.Parse(elementId);
                Elements.Add(elementModel);
            }

            Model.Elements = Elements;
            
            var responseString = (String)null;

            try
            {
                var response = await Startup.client.PutAsJsonAsync("http://localhost:51226/api/websites", Model);
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }
            
            return Content($"{responseString}");
        }


        // DELETES //
        // DELETE: /website/delete/{id}
        [HttpPost("/website/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            string responseMessage = null;

            try
            {
                var response = await Startup.client.DeleteAsync("http://localhost:51226/api/websites/" + id);
                if (response.IsSuccessStatusCode)
                {
                    responseMessage = (response.Content.ReadAsAsync<string>().Result);
                }
                else
                {
                    responseMessage = (response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {

            }

            return Redirect("/website");
        }

    }
}
