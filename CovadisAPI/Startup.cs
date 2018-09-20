﻿using System;
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

                if (!context.Websites.Any())
                {
                    context.Websites.Add(new WebsitesDataModel()
                    {
                        Url = "https://gms.azurewebsites.net/",
                        Element1 = "Hello",
                        Element2 = "Hallo",
                        Element3 = "Bonjour"
                    });
                    context.Websites.Add(new WebsitesDataModel()
                    {
                        Url = "https://gms.azurewebsites.net/",
                        Element1 = "Hello",
                        Element2 = "Halo",
                        Element3 = "Bonjour"
                    });
                }
                   
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
