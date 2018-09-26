using CovadisDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovadisDashboard.Controllers
{
    public class WebsiteController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(WebsiteModel model)
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
