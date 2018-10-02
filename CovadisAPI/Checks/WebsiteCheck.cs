﻿using CovadisAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {

        public async Task<object> CheckWebsite(WebsitesDataModel website)
        {
            //haalt alle elementen op die bij deze website horen
            List<ElementsDataModel> elements;
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
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        
                        var jsonwebsite = new
                        {
                            website.Id,
                            website.Url,
                            Elements = new List<object> { },

                        };
                        
                        //loopt door elke element die de website heeft en contoleert of dat die in de site voor komt
                        foreach (var element in elements)
                        {

                            if (!data.Contains(element.Name))
                            {
                                var elem = new
                                {
                                    element.Id,
                                    element.Name,
                                    Status = "fout"
                                };
                                jsonwebsite.Elements.Add(elem);
                            }
                            else
                            {
                                var elem = new
                                {
                                    element.Id,
                                    element.Name,
                                    Status = "goed"
                                };
                                jsonwebsite.Elements.Add(elem);
                            }
                        }
                        
                        return jsonwebsite;
                    }
                    else
                    {
                        var errorObj = new
                        {
                            error = "Error: Waarschijnlijk is data null"
                        };
                        return errorObj;
                    }
                }
            }
            catch
            {
                var errorObj = new
                {
                    error = "Error met ophalen van url. Dit kan zijn dat er geen https voor staat"
                };
                return errorObj;
            }
        }
    }
}
