using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CovadisDashboard.Models;
using System.Collections.Generic;

namespace CovadisDashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Checks.getDB check = new Checks.getDB();
            //List of all the websites
            List<WebsiteModel> Websites = check.GetObjects<List<WebsiteModel>>("/websites");
            //List of all the Apis
            List<ApiModel> Apis = check.GetObjects<List<ApiModel>>("/apis");
            HomeCombinedModel Model = new HomeCombinedModel();
            //Model to return to the view
            Model.apiModel = Apis;
            Model.websiteModel = Websites;
            
            return View(Model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
