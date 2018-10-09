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
        public ActionResult Details(int id)
        {
            ViewData["id"] = id;

            Checks.ApiCheck check = new Checks.ApiCheck();
            ApiModel Model = check.RequestApi("/api/" + id);

            //ApiModel Model = new ApiModel();
            //Model.Url = "https://www.nu.nl";
            //Model.Name = "Test";
            //Model.Id = 1;


            if (Model.Url == null)
            {
                Response.StatusCode = 404;
                return View("../shared/page404");
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
            ViewData["Message"] = "Here you can edit an existing websites configuration.";
            ViewData["id"] = id;

            Checks.ApiCheck check = new Checks.ApiCheck();

            ApiModel Model = check.RequestApi("/api/" + id);

            if (Model.Url == null)
            {
                Response.StatusCode = 404;
                return View("../Home/Index");
            }

            return View(Model);
        }


        // POSTS //

        // POST: /api/create
        [HttpPost]
        public async Task<IActionResult> Create(int i)
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

            return Content($"{responseString}");
        }


        // DELETES //
        // DELETE: /website/delete/{id}
        [HttpPost("/api/delete/{id:int}")]
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

            return Redirect("/api");
        }
    }
}