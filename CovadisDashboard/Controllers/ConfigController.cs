using CovadisDashboard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Diagnostics;

namespace CovadisDashboard.Controllers
{
    public class ConfigController : Controller
    {
        // GETS //

        // GET: config
        public ActionResult Index()
        {
            ViewData["Message"] = "This is an configuration for the background worker to periodically check the api's and websites.";

            Checks.getDB check = new Checks.getDB();
            List<ConfigModel> Config = check.GetObjects<List<ConfigModel>>("/configurations");

            return View(Config);
        }

        // GET: /config/details/{id}
        public IActionResult Details(int id)
        {
            ViewData["id"] = id;

            Checks.getDB check = new Checks.getDB();
            ConfigModel Model = check.GetObject<ConfigModel>("/configurations/" + id);

            if (Model.ConfigName == null)
            {
                return View("../shared/page404");
            }

            return View(Model);
        }

        // GET: /config/update/{id}
        [HttpGet]
        [Route("/config/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            ViewData["Message"] = "Here you can edit an existing configuration.";
            ViewData["id"] = id;

            Checks.getDB check = new Checks.getDB();
            ConfigModel Model = check.GetObject<ConfigModel>("/configurations/" + id);

            return View(Model);
        }

        // PUTS //
        // PUT: /config/edit/{id}
        [HttpPost("/config/edit/{id:int}")]
        public async Task<IActionResult> Edit(ConfigModel Model, int id)
        {
            if (id != Model.Id)
            {
                return View("../shared/page404");
            }

            Model.ConfigName = Request.Form["ConfigName"];
            Model.Value = Request.Form["Value"];

            var responseString = (String)null;

            try
            {
                var response = await Startup.client.PutAsJsonAsync("http://localhost:51226/api/configurations", Model);
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }

            //return Content($"{responseString}");
            return Redirect("/config");
        }
    }
}
