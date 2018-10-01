using CovadisDashboard.Models;

namespace CovadisDashboard.Checks
{
    public class WebsiteCheck
    {
        public dynamic RequestWebsites()
        {
            var getJson = new getJson();
            
            string ipAdress = "http://localhost:51226/api/websites";
            
            var Website = getJson._download_serialized_json_data<WebsiteModel>(ipAdress);
            
            return Website;
        }

    }
}
