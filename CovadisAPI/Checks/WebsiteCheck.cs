using CovadisAPI.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CovadisAPI.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {

        public async Task<object> CheckWebsite(WebsitesDataModel website)
        {
            //PAK ALLE ELEMENTEN BIJ DEZE WEBSITE
            List<ElementsDataModel> elements;
            using (var context = new ApplicationDbContext())
            {
                elements = context.Elements.Include(e => e.Website).Where(w => w.Website.WebsiteID == website.WebsiteID).ToList();
                //elements = context.Elements.Where(w => w.Website.WebsiteID == website.WebsiteID).ToList();
            }

            // HAALT DE SITE OP EN CHECKT VOOR DE ELEMENTEN DIE IN elementsToCheck staan
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
                            url = website.Url,
                            elementen = new List<object> { },

                        };
                        
                        //LOOPT DOOR DE elementsToCheck EN CHECKT OF DE ELEMENTEN IN DE OPGEHAALDE SITE STAAN
                        foreach (var element in elements)
                        {

                            if (!data.Contains(element.ElementName))
                            {
                                var elem = new
                                {
                                    elementName = element.ElementName,
                                    elementStatus = "fout"

                                };
                                jsonwebsite.elementen.Add(elem);

                            }
                            else
                            {
                                var elem = new
                                {
                                    elementName = element.ElementName,
                                    elementStatus = "goed"

                                };
                                jsonwebsite.elementen.Add(elem);

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
