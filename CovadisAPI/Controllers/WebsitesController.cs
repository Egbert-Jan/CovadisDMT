using CovadisAPI.Checks;
using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovadisAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : ControllerBase
    {

        // GET api/websites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<string>>>> GetAsync()
        {
            List<List<string>> checkedWebsites = new List<List<string>> { };

            WebsiteCheck check = new WebsiteCheck();
            using (var context = new ApplicationDbContext())
            {
                foreach (var website in context.Websites)
                {
                    //RUNT DE CheckWebsite FUNCTION EN DAT RETURNT EEN ARRAY MET DE URL EN PER ELEMENT OF HET GOED OF FOUT IS
                    checkedWebsites.Add(await check.CheckWebsite(website));
                }
            }
            
            return checkedWebsites;
        }




        // GET api/websites/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<List<string>>> GetAsync(int id)
        {
            WebsiteCheck check = new WebsiteCheck();
            using(var context = new ApplicationDbContext())
            {
                var website = context.Websites.Find(id);

                try
                {
                    return await check.CheckWebsite(website);
                }
                catch
                {
                    return new List<string> { "Kan geen websites vinden met het id: " + id };
                }
            }

        
        }




        // POST api/website

        [HttpPost]
        public void Post([FromBody] WebsitesDataModel nieuweWebsite)
        {
            //voegt een service toe
            using (var context = new ApplicationDbContext())
            {
                var website = new WebsitesDataModel()
                {
                    Url = nieuweWebsite.Url,
                    //Elements = nieuweWebsite.Elements
                };
                context.Websites.Add(website);


                //IK HEB DE ELEMENTEN EN DE WEBSITE ID WAAR ZE AAN TOEGEVOEGD MOETEN WORDEN
                foreach(var x in nieuweWebsite.Elements)
                {
                    Debug.WriteLine("test: " + x.ElementName);
                    context.Elements.Add(new ElementsDataModel()
                    {
                        ElementName = x.ElementName,
                        Website = website
                    });
                }

                context.SaveChanges();
            }
        }






    }
}
