using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovadisAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CovadisAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            using (var context = new ApplicationDbContext())
            {
                //Maakt de database als je hem nog niet hebt. 
                context.Database.EnsureCreated();



                if (!context.Services.Any())
                {
                    context.Services.Add(new ServicesDataModel()
                    {
                        Endpoint = "https://gms.azurewebsites.net/"
                    });
                    context.Services.Add(new ServicesDataModel()
                    {
                        Endpoint = "https://gmsapi.azurewebsites.net/Record/last"
                    });
                }



                //ALS DE DATABASE AAN GEMAAKT WORDT VOEGT HET TEST DATA TOE
                if (!context.Websites.Any())
                {
                  
                    ElementsDataModel element1 = new ElementsDataModel { ElementName = "Hello" };

                    ElementsDataModel element2 = new ElementsDataModel { ElementName = "Hallo" };

                    ElementsDataModel element3 = new ElementsDataModel { ElementName = "Bonjour" };

                    context.Websites.Add(new WebsitesDataModel()
                    {
                        Url = "https://gms.azurewebsites.net/",
                        Elements = new List<ElementsDataModel> { element1, element2, element3 }
                    });


                    ElementsDataModel element11 = new ElementsDataModel { ElementName = "Hello" };

                    ElementsDataModel element22 = new ElementsDataModel { ElementName = "Halo" };

                    ElementsDataModel element33 = new ElementsDataModel { ElementName = "Bonjour" };
 
                    context.Websites.Add(new WebsitesDataModel()
                    {
                        Url = "https://gms.azurewebsites.net/",
                        Elements = new List<ElementsDataModel> { element11, element22, element33 }
                    });

                }



                //ElementsDataModel newElement = new ElementsDataModel { ElementName = "Bonjour" };

                ////WebsitesDataModel website = context.Websites.First(w => w.WebsiteID == 2);
                //WebsitesDataModel website = context.Websites.Find(2);
                ////website.LaatsteData = "test";

                //website.Elements.Add(new ElementsDataModel { ElementName = "wow" });
                


                //SAVED TE DATA DIE JE HEBT AANGEPAST
                context.SaveChanges();
            }


            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
