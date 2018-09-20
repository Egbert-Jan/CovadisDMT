using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CovadisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServicesDataModel>> Get()
        {
            //laat alle services zien uit de database
            List<ServicesDataModel> dataBaseData = new List<ServicesDataModel> { };

            using (var context = new ApplicationDbContext())
            {

                foreach (var row in context.Services)
                {
                    dataBaseData.Add(row);
                }
            }
            return dataBaseData;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ServicesDataModel> Get(int id)
        {
            //laat een service zien
            ServicesDataModel data;
            using (var context = new ApplicationDbContext())
            {
                 data = context.Services.First(c => c.Id == id);
            }

            return data;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //voegt een service toe
            //using (var context = new ApplicationDbContext())
            //{
            //    context.Services.Add(new ServicesDataModel
            //    {
            //        Endpoint = "https://gms.azurewebsites.net/"
            //    });
                
            //    context.SaveChanges();
            //}

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
