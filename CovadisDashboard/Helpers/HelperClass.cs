namespace CovadisDashboard.Helpers
{
    public class HelperClass
    {
        public string UrlHttps(string Url)
        {
            if(Url.StartsWith("http://"))
            {
                Url = Url.Replace("http://", "https://");
            }

            return Url;
        }
        
    }
}
