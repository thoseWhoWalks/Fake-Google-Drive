using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers();

            services.AddCors();

            services.AddSignalR();

            services.AddMvc();

            services.AddSingleton<NotifyHub>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();
             
            app.UseEndpoints(routes =>
            {
                routes.MapHub<NotifyHub>("/notify");
            });
        }
    }
}
