using CovadisAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Newtonsoft.Json;
using System;

namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {
        ErrorModel error = new ErrorModel();

        public async Task<object> CheckWebsite(WebsiteModel website)
        {
            int TimesWrongDataFromConfig = 0;

            //haalt alle elementen op die bij deze website horen
            List<ElementModel> elements;
            using (var context = new ApplicationDbContext())
            {
                elements = context.Elements.Include(e => e.Website).Where(w => w.Website.Id == website.Id).ToList();
                TimesWrongDataFromConfig = Convert.ToInt32(context.GlobalConfiguration.Where(w => w.ConfigName == "MessageAfterTrials").First().Value);
                
            }

            // haalt de site op die werd mee gegeven in de website parameter
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = await client.GetAsync(website.Url))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        
                        var web = new WebsiteModel
                        {
                            Id = website.Id,
                            Url = website.Url,
                            Elements = new List<ElementModel> { }
                        };
                        
                        int AmountWrongData = 0;
                        //loopt door elke element die de website heeft en contoleert of dat die in de site voor komt
                        foreach (var element in elements)
                        {
                            
                            if (!data.Contains(element.Name))
                            {
                                AmountWrongData++;

                                var elem = new ElementModel
                                {
                                    Id = element.Id,
                                    Name = element.Name,
                                    Status = "INCORRECT"
                                };
                                web.Elements.Add(elem);
                            }
                            else
                            {
                                var elem = new ElementModel
                                {
                                    Id = element.Id,
                                    Name = element.Name,
                                    Status = "CORRECT"
                                };
                                web.Elements.Add(elem);
                            }

                        }

                        //////////////////

                        if (AmountWrongData > 0)
                        {
                            web.LastDataCorrect = false;

                            using (var context = new ApplicationDbContext())
                            {
                                var websiteToUpdate = context.Websites.Find(website.Id);
                                websiteToUpdate.LastDataCorrect = false;
                                websiteToUpdate.TimesWrongData++;

                                //Data die naar de site gaat
                                web.TimesWrongData = websiteToUpdate.TimesWrongData;

                                if (websiteToUpdate.TimesWrongData > TimesWrongDataFromConfig)
                                {
                                    Debug.WriteLine("Message: " + websiteToUpdate.Id);
                                    SendMessage();
                                }
                                
                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            web.LastDataCorrect = true;

                            using (var context = new ApplicationDbContext())
                            {
                                var websiteToUpdate = context.Websites.Find(website.Id);
                                websiteToUpdate.LastDataCorrect = true;

                                //Data die naar de site gaat
                                websiteToUpdate.TimesWrongData = 0;

                                context.SaveChanges();
                            }
                        }

                        //////////////////

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
                error.Message = "Error met ophalen van url. Dit kan zijn dat er geen https voor staat";
                return error;
            }
        }



        private void SendMessage()
        {

        }




        
    }
}
