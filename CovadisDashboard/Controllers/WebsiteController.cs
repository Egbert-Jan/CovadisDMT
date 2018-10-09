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
            if(id == 0)
            {
                return CustomNotFound();
            }

            ViewData["id"] = id;

            Checks.WebsiteCheck check = new Checks.WebsiteCheck();
            WebsiteModel Model = check.RequestWebsite("/websites/" + id);

            if (Model.Url == null)
            {
                return CustomNotFound();
            }

            return View(Model);
        }

        // GET: /website/add
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Message"] = "Here you can add a websites configuration.";

            return View();
        }

        // GET: /website/edit/{id}
        [HttpGet]
        [Route("/website/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return CustomNotFound();
            }

            ViewData["Message"] = "Here you can update an existing websites configuration.";
            ViewData["id"] = id;

            Checks.WebsiteCheck check = new Checks.WebsiteCheck();

            WebsiteModel Model = check.RequestWebsite("/websites/" + id);

            if (Model.Url == null)
            {
                return CustomNotFound();
            }

            return View(Model);
        }


        // POSTS //

        // POST: /website/add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int elements)
        {
            if (ModelState.IsValid)
            {
                List<ElementModel> Elements = new List<ElementModel>();
                WebsiteModel Model = new WebsiteModel();

                Helpers.HelperClass helper = new Helpers.HelperClass();
                
                Model.Name = Request.Form["Name"];
                Model.Url = helper.UrlHttps(Request.Form["Url"]);

                for (int i = 1; i <= elements; i++)
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
                catch (Exception e)
                {

                }

                //return Content($"{responseString}");
                return Redirect("/website");
            }
            else
            {
                WebsiteModel Model = new WebsiteModel();
                Model.Url = Request.Form["Url"];
                Model.Name = Request.Form["Name"];
                return View(Model);
            }
        }


        // PUTS //
        // PUT: /website/edit/{id}
        [HttpPost("/website/edit/{id:int}")]
        public async Task<IActionResult> Edit(WebsiteModel Model, int elements, int id)
        {
            if(id != Model.Id)
            {
                return CustomNotFound();
            }

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

            //return Content($"{responseString}");
            return Redirect("/website");
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
                    responseMessage = (response.RequestMessage.ToString());
                }
                else
                {
                    responseMessage = (response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {

            }

            //return Content($"{responseString}");
            return RedirectToAction("/index");
        }

        // When page is not found, return page404 not found
        public IActionResult CustomNotFound()
        {
            return View("../shared/page404");
        }
    }
}
