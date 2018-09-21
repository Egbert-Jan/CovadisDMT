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
                foreach (var element in context.Elements)
                {
                    if (element.WebsiteId == website.Id)
                    {
                        elementsToCheck.Add(element);
                    }
                }
            }


            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(website.Url))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();

                if (data != null)
                {
                    websiteData.Add(website.Url);

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
                return websiteData;
            }
        }
    }
}
