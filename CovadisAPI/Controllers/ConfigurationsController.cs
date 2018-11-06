using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovadisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController
    {
        

        // api/configurations
        [HttpGet]
        public string Get()
        {
            List<object> configList = new List<object> { };

            using(var context = new ApplicationDbContext())
            {
                foreach(var row in context.GlobalConfiguration)
                {
                    configList.Add(row);
                }

            }

            return JsonConvert.SerializeObject(configList);
        }


        [HttpGet("{id}")]
        public object Get(int Id)
        {
            using (var context = new ApplicationDbContext())
            {
                var api = context.GlobalConfiguration.Find(Id);
                return api;
            }
        }



        [HttpPut]
        public string Put([FromBody] ConfigurationModel nieuweConfig)
        {
            using(var context = new ApplicationDbContext())
            {
                var oudeConfig = context.GlobalConfiguration.Find(nieuweConfig.Id);

                if(oudeConfig != null)
                {
                    oudeConfig.Value = nieuweConfig.Value;
                }

                context.SaveChanges();

            }

            return "put";
        }
        

        
    }
}
