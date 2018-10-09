using CovadisDashboard.Models;
using System.Collections.Generic;

namespace CovadisDashboard.Checks
{
    public class ApiChecks
    {
        public dynamic RequestApis(string url)
        {
            var GetJson = new GetJson();
            string apiUrl = "http://localhost:51226/api";
            string requestUrl = apiUrl + url;

            var Apis = GetJson._download_serialized_json_data<List<ApiModel>>(requestUrl);

            return Apis;
        }
    }
}
