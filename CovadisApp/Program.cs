using System;
using System.Net.Http;

namespace CovadisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = TimeSpan.Zero;
            var tijd = TimeSpan.FromSeconds(10);

            var timer = new System.Threading.Timer(async (e) => { await CallAPIAsync(); }, null, start, tijd);


            Console.ReadLine();
        }


        static public async System.Threading.Tasks.Task CallAPIAsync()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync("https://localhost:44334/api/websites"))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
        }
    }
}
