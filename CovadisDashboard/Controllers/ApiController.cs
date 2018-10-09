using CovadisDashboard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CovadisDashboard.Controllers
{
    public class ApiController : Controller
    {

        // GETS //

        // GET: Api
        public ActionResult Index()
        {
            ViewData["Message"] = "This is an overview of all the API's that are getting checked.";

            Checks.ApiChecks check = new Checks.ApiChecks();
            List<ApiModel> Apis = check.RequestApis("/apis");

            return View(Apis);
        }

        // GET: /api/details/{id}
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return CustomNotFound();
            }

            ViewData["id"] = id;

            Checks.ApiCheck check = new Checks.ApiCheck();
            ApiModel Model = check.RequestApi("/api/" + id);

            //ApiModel Model = new ApiModel();
            //Model.Url = "https://www.nu.nl";
            //Model.Name = "Test";
            //Model.Id = 1;


            if (Model.Url == null)
            {
                return CustomNotFound();
            }

            return View(Model);
        }

        // GET: api/create
        public ActionResult Create()
        {
            ViewData["Message"] = "Here you can add a api configuration.";

            return View();
        }

        // GET: /website/update/{id}
        [HttpGet]
        [Route("/api/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return CustomNotFound();
            }

            ViewData["Message"] = "Here you can edit an existing websites configuration.";
            ViewData["id"] = id;

            Checks.ApiCheck check = new Checks.ApiCheck();

            ApiModel Model = check.RequestApi("/api/" + id);

            if (Model.Url == null)
            {
                return CustomNotFound();
            }

            return View(Model);
        }


        // POSTS //

        // POST: /api/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int i)
        {
            if (ModelState.IsValid)
            {
                ApiModel Model = new ApiModel();

                Model.Name = Request.Form["Name"];
                Model.Url = Request.Form["Url"];
            
                var responseString = (String)null;

                try
                {
                    var response = await Startup.client.PostAsJsonAsync("http://localhost:51226/api/api", Model);
                    responseString = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {

                }

                //return Content($"{responseString}");
                return Redirect("/api");
            }
            else
            {
                return View();
            }
        }


        // PUTS //
        // PUT: /website/update/{id}
        [HttpPost("/api/edit/{id:int}")]
        public async Task<IActionResult> Edit(ApiModel Model, int id)
        {
            
            Model.Name = Request.Form["Name"];
            Model.Url = Request.Form["Url"];
            
            var responseString = (String)null;

            try
            {
                var response = await Startup.client.PutAsJsonAsync("http://localhost:51226/api/api", Model);
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }

            //return Content($"{responseString}");
            return Redirect("/api");
        }


        // DELETES //
        // DELETE: /website/delete/{id}
        [HttpPost("/api/delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            string responseString = null;

            try
            {
                var response = await Startup.client.DeleteAsync("http://localhost:51226/api/websites/" + id);
                if (response.IsSuccessStatusCode)
                {
                    responseString = (response.Content.ReadAsAsync<string>().Result);
                }
                else
                {
                    responseString = (response.StatusCode.ToString());
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