using CovadisAPI.Checks;
using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public async Task<ActionResult<string>> GetAsync()
        {
            List<object> checkedWebsites = new List<object> { };

            WebsiteCheck check = new WebsiteCheck();
            using (var context = new ApplicationDbContext())
            {
                foreach (var website in context.Websites)
                {
                    //RUNT DE CheckWebsite FUNCTION EN DAT RETURNT EEN ARRAY MET DE URL EN PER ELEMENT OF HET GOED OF FOUT IS
                    checkedWebsites.Add(await check.CheckWebsite(website));
                }
            }
            
            //Returnt het in JSON
            return JsonConvert.SerializeObject(checkedWebsites);
        }




        // GET api/websites/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAsync(int id)
        {
            WebsiteCheck check = new WebsiteCheck();

            using(var context = new ApplicationDbContext())
            {
                //Pakt een website bij dit ID
                var website = context.Websites.Find(id);

                //Controleert de site op elementen
                try
                {
                    return await check.CheckWebsite(website);
                }
                catch
                {
                    var errorObj = new
                    {
                        error = "Fout met het ophalen. Waarschijnlijk bestaat de website niet met dit ID"
                    };

                    return errorObj;
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
                //Maakt een nieuwe website aan en voegt hem toe aan de context
                var website = new WebsitesDataModel()
                {
                    Url = nieuweWebsite.Url,
                };
                context.Websites.Add(website);


                //Voegt de elementen toe aan de site die net toegevoegt is aan de context
                foreach(var x in nieuweWebsite.Elements)
                {
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
