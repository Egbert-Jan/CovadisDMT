using CovadisDashboard.Models;

namespace CovadisDashboard.Checks
{
    public class ApiCheck
    {
        public ApiModel RequestApi(string url)
        {
            GetJson GetJson = new GetJson();
            string apiUrl = "http://localhost:51226/api";
            string requestUrl = apiUrl + url;

            var Api = GetJson._download_serialized_json_data<ApiModel>(requestUrl);

            return Api;
        }
    }
}
