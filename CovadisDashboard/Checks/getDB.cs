using CovadisDashboard.Models;

namespace CovadisDashboard.Checks
{
    public class getDB
    {
        public T GetObjects<T>(string url) where T : new()
        {
            GetJson GetJson = new GetJson();
            string apiUrl = "http://localhost:51226/api";
            string requestUrl = apiUrl + url;

            var Api = GetJson._download_serialized_json_data<T>(requestUrl);

            return Api;
        }

        public T GetObject<T>(string url) where T : new()
        {
            GetJson GetJson = new GetJson();
            string apiUrl = "http://localhost:51226/api";
            string requestUrl = apiUrl + url;

            var Api = GetJson._download_serialized_json_data<T>(requestUrl);

            return Api;
        }

    }
}
