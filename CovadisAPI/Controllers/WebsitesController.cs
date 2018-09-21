﻿using CovadisAPI.Checks;
using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<ActionResult<IEnumerable<List<string>>>> GetAsync()
        {
            List<List<string>> checkedWebsites = new List<List<string>> { };

            WebsiteCheck check = new WebsiteCheck();

            using (var context = new ApplicationDbContext())
            {

                foreach (var website in context.Websites)
                {
                    checkedWebsites.Add(await check.CheckWebsite(website));
                }

            }

            return checkedWebsites;
        }




        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {



            using (var context = new ApplicationDbContext())
            {
                IList<ElementsDataModel> elements = context.Elements.Include(c => c.WebsiteId).ToList();
                return "" + elements;
            }





            return "website";
        }
    }



  
}