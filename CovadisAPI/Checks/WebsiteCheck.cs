using CovadisAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;
using Newtonsoft.Json;

namespace CovadisAPI.Checks
{
    public class WebsiteCheck
    {
        ErrorModel error = new ErrorModel();
        string Token;

        public WebsiteCheck()
        {

        }

        public WebsiteCheck(string token)
        { 
           this.Token = token;
        }

        public async Task<object> CheckWebsite(WebsiteModel website)
        {
            

            //haalt alle elementen op die bij deze website horen
            List<ElementModel> elements;
            using (var context = new ApplicationDbContext())
            {
                elements = context.Elements.Include(e => e.Website).Where(w => w.Website.Id == website.Id).ToList();
                //elements = context.Elements.Where(w => w.Website.WebsiteID == website.WebsiteID).ToList();
            }

            // haalt de site op die werd mee gegeven in de website parameter
            try
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = await client.GetAsync(website.Url))
                using (HttpContent content = res.Content)
                {
                    //Gehele website in string
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        var web = new WebsiteModel
                        {
                            Id = website.Id,
                            Url = website.Url,
                            Error = null,
                            Elements = new List<ElementModel> { }
                        };
                        
                        //loopt door elke element die de website heeft en contoleert of dat die in de site voor komt
                        foreach (var element in elements)
                        {
                            var elem = new ElementModel();

                            if (!data.Contains(element.Name))
                            {
                                elem.Id = element.Id;
                                elem.Name = element.Name;
                                elem.Status = "INCORRECT";

                                web.Elements.Add(elem);
                            }
                            else
                            {
                                elem.Id = element.Id;
                                elem.Name = element.Name;
                                elem.Status = "CORRECT";
                              
                                web.Elements.Add(elem);
                            }
                        }

                        

                        //AddToLog
                        AddToLog(web);

                        //SendMail(web);

                        return web;
                    }
                    else
                    {
                        error.Message = "Error: Waarschijnlijk is data null";
                        return error;
                    }
                }
            }
            catch
            {
                var faultyWebsite = new WebsiteModel
                {
                    Id = website.Id,
                    Url = website.Url,
                    Error = "URL Error",
                    Elements = new List<ElementModel> { }
                };

                foreach (var element in elements)
                {
                    var elem = new ElementModel
                    {
                        Id = element.Id,
                        Name = element.Name,
                        Status = "Error: Could not check element"
                    };

                    faultyWebsite.Elements.Add(elem);
                }
                

                //AddToLog
                AddToLog(faultyWebsite);

                //SendMail(faultyWebsite);

                return faultyWebsite;

            //End catch
            }
        //End method
        }


        private void AddToLog(WebsiteModel website)
        {
            //if(Token != null && Token == "lolxddezeapptokenislit12345")
            //{
                DateTime dateTime = DateTime.UtcNow;

                Debug.WriteLine(dateTime);
            SendMail(website);
            //SendMail(website);

            using (var context = new ApplicationDbContext())
                {
                    var web = new WebsiteLog()
                    {
                        WebsiteID = website.Id,
                        Url = website.Url,
                        Error = website.Error,
                        TimeStamp = dateTime.ToString()
                    };
                    context.WebsiteLog.Add(web);

                    foreach (var element in website.Elements)
                    {
                        context.ElementLog.Add(new ElementLog()
                        {
                            Name = element.Name,
                            Status = element.Status,
                            WebsiteID = web.WebsiteID,
                            TimeStamp = dateTime.ToString()
                        });
                    }

                    context.SaveChanges();
                }

            
            //Eind if
            //}
        }




        public void SendMail(WebsiteModel website)
        {
            Boolean hasError = false;
            Boolean hasSendMail = false;
  

            using (var context = new ApplicationDbContext())
            {

                var MessageAfterTrials = Int32.Parse(context.GlobalConfiguration.Find(2).Value);

                var websites = context.WebsiteLog.Where(w => w.WebsiteID == website.Id).OrderByDescending(w => w.Id).Take(MessageAfterTrials).ToList();



                Debug.WriteLine("");
                foreach (var web in websites)
                {
                    Debug.WriteLine(web.Id + " en " + web.WebsiteID + ", " + web.Url + ", mail: " + web.HasSendMail + ", error: " + web.Error);

                    if(web.Error != null)
                    {
                        hasError = true;
                    }
                    if(web.HasSendMail == true)
                    {
                        hasSendMail = true;
                    }
                }
                Debug.WriteLine("");

                //ALS DE SITE EEN FOUTE LOG HEEFT
                if(hasError == true)
                {
                    //Debug.WriteLine("Website " + website.Url + " Heeft een error");

                    if(hasSendMail == false)
                    {
                        Debug.WriteLine("Website " + website.Url + " Mail gestuurd!!!!!!!!");
                        MailData mail = new MailData();

                        String bodyString = 
                            "<h1>Hello,</h1><br>" +
                            "<p>The site " + website.Url + " is offline for " + MessageAfterTrials + " trials.</p> <br>" +
                            "<p>Go to the <a href='https://localhost:44315/Website'>Dashboard</a> for more details.</p>" +
                            "<p>You get this message because blablabla</p>";



                        mail.SendMail("Website: " + website.Url + " is offline for " + MessageAfterTrials + " trials", bodyString);

                        //Maak hasSendMail true
                        foreach (var web in websites)
                        {
                            web.HasSendMail = true;
                            context.Update(web);
                        }

                    }
                }


                context.SaveChanges();
            }
        }
    }
}















 //Boolean heeftError = false;
 //           Boolean hasSendMail = false;

 //           //Als site 2 keer fout is geweest
 //           using (var context = new ApplicationDbContext())
 //           {
 //               var websites = context.WebsiteLog.Where(w => w.WebsiteID == website.Id).OrderByDescending(w => w.Id).Take(2).ToList();

 //               foreach (var web in websites)
 //               {
 //                   Debug.WriteLine("id: " + web.Id + " error: " + web.Error);
 //                   //Debug.Write(JsonConvert.SerializeObject(web));
 //                   if (web.Error != null)
 //                   {
 //                       heeftError = true;
 //                   }
 //                   if(web.HasSendMail == true)
 //                   {
 //                       hasSendMail = true;
 //                   }
 //               }

 //               if(heeftError == true)
 //               {
 //                   Debug.WriteLine("ID" + website.Id + " in de logs is die een of meer keer fout");
 //                   if (hasSendMail == false)
 //                   {
 //                       Debug.WriteLine("mail is gestuurd voor het ID: " + websites[0].WebsiteID);


 //                       foreach (var web in websites)
 //                       {
 //                           Debug.WriteLine("id: " + web.Id + "mail send is: " + web.HasSendMail);
 //                           web.HasSendMail = true;
 //                           context.Update(web);

 //                           context.SaveChanges();

 //                       }

 //                       MailData md = new MailData();
 //                       md.SendMail("Error met een website", "WebsiteID: " + website.Id);
 //                   }
 //               }

               
 //           }


//}