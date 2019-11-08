using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LageHelersonBoosterTest2019.Model;
using LageHelersonBoosterTest2019.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LageHelersonBoosterTest2019
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

            services.AddTransient<ILorumIpsumDataModel, LorumIpsumDataModel>();
            services.AddTransient<IDataService, DataService > ();

           // services.AddScoped<ILorumIpsumDataModel, LorumIpsumDataModel>();
           // services.AddScoped<IDataService, DataService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
