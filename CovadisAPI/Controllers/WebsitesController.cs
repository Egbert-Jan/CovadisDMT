﻿using CovadisAPI.Checks;
using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("{Id}")]
        public async Task<ActionResult<object>> GetAsync(int Id)
        {
            WebsiteCheck check = new WebsiteCheck();

            using(var context = new ApplicationDbContext())
            {
                //Pakt een website bij dit ID
                var website = context.Websites.Find(Id);

                //Controleert de site op elementen
                try
                {
                    return JsonConvert.SerializeObject(await check.CheckWebsite(website));
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
        public void Post([FromBody] WebsiteModel nieuweWebsite)
        {
            //voegt een service toe
            using (var context = new ApplicationDbContext())
            {
                //Maakt een nieuwe website aan en voegt hem toe aan de context
                var website = new WebsiteModel()
                {
                    Url = nieuweWebsite.Url,
                };
                context.Websites.Add(website);


                //Voegt de elementen toe aan de site die net toegevoegt is aan de context
                foreach(var x in nieuweWebsite.Elements)
                {
                    context.Elements.Add(new ElementModel()
                    {
                        Name = x.Name,
                        Website = website
                    });
                }


                context.SaveChanges();
            }
        }


        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            using(var context = new ApplicationDbContext())
            {
                try
                {
                    var website = context.Websites.Find(id);

                    List<ElementModel> elements = context.Elements.Include(e => e.Website).Where(w => w.Website.Id == website.Id).ToList();

                    foreach (var element in elements)
                    {
                        context.Elements.Remove(element);
                    }
                    context.Websites.Remove(website);

                    context.SaveChanges();

                    var errorObj = new
                    {
                        error = "Succesvol verwijderd"
                    };
                    return errorObj;
                }
                catch
                {
                    var errorObj = new
                    {
                        error = "Fout met verwijderen"
                    };
                    return errorObj;
                }
            }
        }





        // PUT api/websites/5
        [HttpPut]
        public void Put([FromBody] WebsiteModel website)
        {
            using (var context = new ApplicationDbContext())
            {
                var oldConfig = context.Websites.Find(website.Id);
                List<ElementModel> elements = context.Elements.Include(e => e.Website).Where(w => w.Website.Id == website.Id).ToList();

                if(oldConfig != null)
                {
                    oldConfig.Url = website.Url;

                    foreach (var element in elements)
                    {
                        context.Elements.Remove(element);
                    }

                    oldConfig.Elements = website.Elements;

                    context.SaveChanges();
                }
            }
        }









    //Class
    }

//namespace
}
