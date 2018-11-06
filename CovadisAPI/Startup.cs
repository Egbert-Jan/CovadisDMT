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


                //ALS DE DATABASE AAN GEMAAKT WORDT VOEGT HET TEST DATA TOE
                if (!context.Websites.Any())
                {
                    beginDataVoorDatabase();
                }



                //VOEG EEN NIEUW ELEMENT TOE AAN EEN BESTAANDE WEBSITE
                //WebsitesDataModel website = context.Websites.Find(2);

                //context.Elements.Add(new ElementsDataModel()
                //{
                //    ElementName = "test",
                //    Website = website
                //});


                //VERWIJDER EEN ELEMENT:
                //ElementsDataModel element = context.Elements.Find(1003);
                //context.Elements.Remove(element);


                //UPDATE EEN ELEMENT:
                //ElementsDataModel element = context.Elements.Find(1003);
                //element.ElementName = "nieuwe test";



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


        private void beginDataVoorDatabase()
        {

            using (var context = new ApplicationDbContext())
            {
                context.GlobalConfiguration.Add(new ConfigurationModel()
                {
                    ConfigName = "CheckTime",
                    Value = "15"
                });

                context.GlobalConfiguration.Add(new ConfigurationModel()
                {
                    ConfigName = "MessageAfterTrials",
                    Value = "3"
                });


                ElementModel element1 = new ElementModel { Name = "Hello" };

                ElementModel element2 = new ElementModel { Name = "Hallo" };

                ElementModel element3 = new ElementModel { Name = "Bonjour" };

                context.Websites.Add(new WebsiteModel()
                {
                    Url = "https://gms.azurewebsites.net/",
                    Elements = new List<ElementModel> { element1, element2, element3 }
                });


                ElementModel element11 = new ElementModel { Name = "Hello" };

                ElementModel element22 = new ElementModel { Name = "Halo" };

                ElementModel element33 = new ElementModel { Name = "Bonjour" };

                context.Websites.Add(new WebsiteModel()
                {
                    Url = "https://www.google.com/",
                    Elements = new List<ElementModel> { element11, element22, element33 }
                });


                context.Apis.Add(new ApiModel()
                {
                    Url = "https://www.google.com/"
                });

                context.Apis.Add(new ApiModel()
                {
                    Url = "https://gmsapi.azurewebsites.net/Record/3"
                });

                context.SaveChanges();
            }
        }
    }
}
