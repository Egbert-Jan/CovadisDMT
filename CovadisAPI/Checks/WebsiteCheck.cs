using CovadisAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;

namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {
        ErrorModel error = new ErrorModel();
        string Token;

        public WebsiteCheck()
        {

        }

        public WebsiteCheck(string token)
        { 
           this.Token = token;
        }

        public async Task<object> CheckWebsite(WebsiteModel website)
        {
            

            //haalt alle elementen op die bij deze website horen
            List<ElementModel> elements;
            using (var context = new ApplicationDbContext())
            {
                elements = context.Elements.Include(e => e.Website).Where(w => w.Website.Id == website.Id).ToList();
                //elements = context.Elements.Where(w => w.Website.WebsiteID == website.WebsiteID).ToList();
            }

            // haalt de site op die werd mee gegeven in de website parameter
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = await client.GetAsync(website.Url))
                using (HttpContent content = res.Content)
                {
                    //Gehele website in string
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        var web = new WebsiteModel
                        {
                            Id = website.Id,
                            Url = website.Url,
                            Error = null,
                            Elements = new List<ElementModel> { }
                        };
                        
                        //loopt door elke element die de website heeft en contoleert of dat die in de site voor komt
                        foreach (var element in elements)
                        {
                            var elem = new ElementModel();

                            if (!data.Contains(element.Name))
                            {
                                elem.Id = element.Id;
                                elem.Name = element.Name;
                                elem.Status = "INCORRECT";

                                web.Elements.Add(elem);
                            }
                            else
                            {
                                elem.Id = element.Id;
                                elem.Name = element.Name;
                                elem.Status = "CORRECT";
                              
                                web.Elements.Add(elem);
                            }
                        }

                        //AddToLog
                        AddToLog(web);
                        
                        return web;
                    }
                    else
                    {
                        error.Message = "Error: Waarschijnlijk is data null";
                        return error;
                    }
                }
            }
            catch
            {
                var faultyWebsite = new WebsiteModel
                {
                    Id = website.Id,
                    Url = website.Url,
                    Error = "URL Error",
                    Elements = new List<ElementModel> { }
                };

                foreach (var element in elements)
                {
                    var elem = new ElementModel
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Status = "Error: Could not check element"
                    };

                    faultyWebsite.Elements.Add(elem);
                }

                //AddToLog
                AddToLog(faultyWebsite);

                return faultyWebsite;

            //End catch
            }
        //End method
        }


        private void AddToLog(WebsiteModel website)
        {
            if(Token != null && Token == "lolxddezeapptokenislit12345")
            {
                DateTime dateTime = DateTime.UtcNow;

                Debug.WriteLine(dateTime);

                using (var context = new ApplicationDbContext())
                {
                    var web = new WebsiteLog()
                    {
                        WebsiteID = website.Id,
                        Url = website.Url,
                        Error = website.Error,
                        TimeStamp = dateTime.ToString()
                    };
                    context.WebsiteLog.Add(web);

                    foreach (var element in website.Elements)
                    {
                        context.ElementLog.Add(new ElementLog()
                        {
                            Name = element.Name,
                            Status = element.Status,
                            WebsiteID = web.WebsiteID,
                            TimeStamp = dateTime.ToString()
                        });
                    }

                    context.SaveChanges();
                }

            //Eind if
            }
        }
    }
}
