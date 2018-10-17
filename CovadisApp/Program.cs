using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CovadisApp
{
    class Program
    {

        static TimeSpan CheckTime = TimeSpan.FromSeconds(10);

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    CallAPIAsync();
                    Thread.Sleep(CheckTime);
                    Console.WriteLine("checktime = " + CheckTime);
                }
                catch
                {
                    Console.WriteLine("error");
                }
            }
        }


        static public async void CallAPIAsync()
        {
            using (HttpClient client = new HttpClient())
            {

                using (HttpResponseMessage res = await client.GetAsync("https://localhost:44334/api/configurations"))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(data);

                    foreach (var obj in json)
                    {
                        if (obj.ConfigName == "CheckTime")
                        {
                            CheckTime = TimeSpan.FromSeconds((int)obj.Value);
                            Console.WriteLine("New CheckTime = " + CheckTime);
                        }
                    }
                }


                //Header die er voor zorgt dat er een log wordt weg geschreven
                //client.DefaultRequestHeaders.Add("app-token", "lolxddezeapptokenislit12345");

                using (HttpResponseMessage res = await client.GetAsync("https://localhost:44334/api/websites"))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    Console.WriteLine(data);
                }
            }
        }

    //classe
    }

//namespace
}
