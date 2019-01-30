using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace apione
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
            services.AddSingleton<IHealthContributor, CustomHealthContributor>();
            services.AddSingleton<IHealthCheckHandler, EurekaHealthCheckHandler>();

            //services.AddHealthActuator(Configuration);

            services.AddSingleton<IClientService, ClientService>();
            // Add Steeltoe Discovery Client service
            services.AddDiscoveryClient(Configuration);
            // Add Steeltoe handler to container
            services.AddTransient<DiscoveryHttpMessageHandler>();
            var config = Configuration["Services:ClientService:Url"];
            // Configure a HttpClient
            services.AddHttpClient("client-api-values", c =>
                {
                    c.BaseAddress = new Uri(config);
                })
                .AddHttpMessageHandler<DiscoveryHttpMessageHandler>()
                .AddTypedClient<IClientService, ClientService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDiscoveryClient();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseHttpsRedirection();

            // Add management endpoint into pipeline
            //app.UseHealthActuator();
          
            app.UseMvc();
        }
    }
}
