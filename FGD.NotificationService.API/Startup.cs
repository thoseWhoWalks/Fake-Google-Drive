using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FGD.NotificationService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<NotifyHub>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors(
                  options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins(
                   "http://localhost",
                  "http://localhost:5000",
                  "http://localhost:4200",
                  "http://fakegoogledrive.eastus.cloudapp.azure.com:5000",
                  "http://fakegoogledrive.eastus.cloudapp.azure.com:5001",
                  "http://fakegoogledrive.eastus.cloudapp.azure.com")
              );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
             
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notify");
            });

            app.UseMvc();
        }
    }
}
