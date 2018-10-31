using CovadisAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovadisAPI.Controllers
{
    [Route("api/[controller]")]
    public class ApisController
    {
        ErrorModel error = new ErrorModel();

        // GET: api/<controller>
        [HttpGet]
        public async Task<List<object>> GetAsync()
        {
            List<object> Apis = new List<object> { };

            using (var context = new ApplicationDbContext())
            {
                foreach (var api in context.Apis)
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        using (HttpResponseMessage res = await client.GetAsync(api.Url))
                        {
                            var apimodel = new ApiModel
                            {
                                Id = api.Id,
                                Name = api.Name,
                                Url = api.Url,
                                Status = res.StatusCode.ToString()
                            };

                            Apis.Add(apimodel);
                        };

                    }
                    catch
                    {
                        var apimodel = new ApiModel
                        {
                            Id = api.Id,
                            Name = api.Name,
                            Url = api.Url,
                            Status = "NULL",
                            Error = "Error met de url"
                        };
                        Apis.Add(apimodel);
                    }
                }
                return Apis;
            }
        }



        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<object> GetAsync(int Id)
        {
            using (var context = new ApplicationDbContext())
            {
                var api = context.Apis.Find(Id);

                try
                {
                    using (HttpClient client = new HttpClient())
                    using (HttpResponseMessage res = await client.GetAsync(api.Url))
                    {
                        var apimodel = new ApiModel()
                        {
                            Url = api.Url,
                            Status = res.StatusCode.ToString()
                        };
                        return apimodel;
                    }
                    
                }
                catch
                {
                    error.Message = "de api bestaat niet";
                    return error;
                }
                
            }
        }
    




        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] ApiModel nieuweApi)
        {
            using (var context = new ApplicationDbContext())
            {
                var api = new ApiModel()
                {
                    Url = nieuweApi.Url,
                    Name = nieuweApi.Name,
                };
                context.Apis.Add(api);

                context.SaveChanges();
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void Put([FromBody] ApiModel api)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Apis.Update(api);
                context.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var api = context.Apis.Find(id);
                context.Apis.Remove(api);
                context.SaveChanges();
            }
        }
    }
}


