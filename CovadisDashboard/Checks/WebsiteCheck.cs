﻿using CovadisDashboard.Models;

namespace CovadisDashboard.Checks
{
    public class WebsiteCheck
    {
        public WebsiteModel RequestWebsite(string url)
        {
            GetJson GetJson = new GetJson();
            string apiUrl = "http://localhost:51226/api";
            string requestUrl = apiUrl + url;

            var Website = GetJson._download_serialized_json_data<WebsiteModel>(requestUrl);

            return Website;
        }
    }
}
