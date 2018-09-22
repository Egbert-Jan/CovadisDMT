using CovadisAPI.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CovadisAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {

        public async Task<List<string>> CheckWebsite(WebsitesDataModel website)
        {
            List<ElementsDataModel> elementsToCheck = new List<ElementsDataModel> { };
            List<string> websiteData = new List<string> { };




            using (var context = new ApplicationDbContext())
            {
                //VOEGT ALLEEN DE ELEMENTEN MET HET ZELFDE ID ALS DEZE WEBSITE TOE AAN elementsToCheck
                foreach (var element in context.Elements)
                {
                    if (element.WebsiteId == website.Id)
                    {
                        elementsToCheck.Add(element);
                    }
                }
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
                    foreach (var element in elementsToCheck)
                    {
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
