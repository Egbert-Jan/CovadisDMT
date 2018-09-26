using CovadisDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovadisDashboard.Controllers
{
    public class WebsiteController : Controller
    {
        // GET: /websites/
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"] = "I don't know what content will be displayed here, if any at all.";

            Checks.WebsiteCheck check = new Checks.WebsiteCheck();
            ViewData["data"] = check.RequestWebsites();

            return View();
        }
        
        // GET: /websites/{id}
        [HttpGet("/website/view/{id:int}")]
        public IActionResult Index(string id)
        {
            ViewData["id"] = id;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Message"] = "Use this page to add a new website to the configuration.";

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

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(WebsiteModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                return Content($"The name: {model.Name}, your url: {model.Url}, the 1st: {model.Element1}, the 2nd: {model.Element2}, the last: {model.Element3}!");
            }
        }
    }
}
