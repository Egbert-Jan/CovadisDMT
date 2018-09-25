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

        public async Task<List<string>> CheckWebsite(WebsitesDataModel website)
        {
            List<string> websiteData = new List<string> { };

            //PAK ALLE ELEMENTEN
            IList<ElementsDataModel> elements;
            using (var context = new ApplicationDbContext())
            {
                elements = context.Elements.Include(e => e.Website).Where(w => w.Website.WebsiteID == website.WebsiteID).ToList();
                
                //elements = context.Elements.Where(w => w.Website.WebsiteID == website.WebsiteID).ToList();
            }


            // HAALT DE SITE OP EN CHECKT VOOR DE ELEMENTEN DIE IN elementsToCheck staan
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(website.Url))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();

                if (data != null)
                {
                    websiteData.Add(website.Url);
                    
                    //LOOPT DOOR DE elementsToCheck EN CHECKT OF DE ELEMENTEN IN DE OPGEHAALDE SITE STAAN
                    foreach (var element in elements)
                    {
                        Debug.WriteLine(element.Website.Url);
                        
                        if (!data.Contains(element.ElementName))
                        {
                            websiteData.Add("Fout");
                        }
                        else
                        {
                            websiteData.Add("Goed");
                        }
                    }
                }

                //RETURNT EEN ARRAY MET DE WEBSITE URL EN PER ELEMENT OF HET GOED OF FOUT IS
                return websiteData;
            }
        }
    }
}
