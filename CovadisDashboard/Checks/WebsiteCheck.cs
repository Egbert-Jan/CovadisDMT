﻿using CovadisDashboard.Models;

namespace CovadisDashboard.Checks
{
    public class WebsiteCheck
    {
        public dynamic RequestWebsites(string url)
        {
            var GetJson = new GetJson();
            string apiUrl = "http://localhost:51226";
            string requestUrl = apiUrl + url;

            var Website = GetJson._download_serialized_json_data<WebsiteModel>(requestUrl);

            return Website;
        }

    }
}