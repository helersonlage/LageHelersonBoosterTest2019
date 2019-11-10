using LageHelersonBoosterTest2019.Model;
using LageHelersonBoosterTest2019.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace LageHelersonBoosterTest2019
{
    public class Startup
    {
        private const string Name = "Booster Test: Api v1 ";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           // services.AddTransient<ILorumIpsumDataModel, LorumIpsumDataModel>();
           // services.AddTransient<IDataService, DataService>();

             services.AddScoped<ILorumIpsumDataModel, LorumIpsumDataModel>();
             services.AddScoped<IDataService, DataService>();

            services.AddMvc();


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Name,
                    Description = "Code test 2019",                    
                    Contact = new OpenApiContact
                    {
                        Name = "Helerson Mendonca Lage",
                        Email = "helersonlage@gmail.com" ,
                        Url = new Uri("https://www.linkedin.com/in/helersonlage/"),
                    }                    
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Name);               
            });


            app.UseMvc();
        }
    }
}
