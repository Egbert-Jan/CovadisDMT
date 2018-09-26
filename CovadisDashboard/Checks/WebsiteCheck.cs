using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CovadisDashboard.Checks
{
    public class WebsiteCheck
    {

        public dynamic RequestWebsites()
        {
            //ipAdress = ConfigurationManager.AppSettings["ApiUrl"];
            string ipAdress = "http://localhost:51226";

            object response;
            
            using (WebClient wc = new WebClient())
            {
                try
                {
                    // Add header with access token
                    wc.Headers.Add("Accept", "application/json");

                    response = wc.DownloadString(ipAdress + "/api/websites/");
                }
                catch (WebException we)
                {
                    response = null;
                }

                return response;
            }
        }

    }
}
