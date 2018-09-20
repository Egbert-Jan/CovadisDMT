using CovadisAPI.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {

        public WebsiteCheck()
        {

        }


        public async Task<List<string>> CheckWebsite(WebsitesDataModel website)
        {
            List<string> websiteData = new List<string> { };

            using (HttpClient client = new HttpClient())

            using (HttpResponseMessage res = await client.GetAsync(website.Url))

            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();

                if (data != null)
                {
                    websiteData.Add(website.Url);
                    //Checkt of dat de website die ingevoert is de data contains die in de database staat
                    if (data.Contains(website.Element1) && data.Contains(website.Element2) && data.Contains(website.Element3))
                    {
                        websiteData.Add("Goed");
                    }
                    else
                    {
                        websiteData.Add("Fout");
                    }
                }
                return websiteData;
            }

        }
    }
}
