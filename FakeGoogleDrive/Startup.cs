using FGD.Api.Configuration;
using FGD.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FakeGoogleDrive
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
            ServiceConfigurationExtention.Configuration = Configuration;

            services.AddControllers();

            services.SetUpOptions();

            services.RegisterDatabaseContext();

            services.ConfigureCors();

            services.ConfigureAuthentication();

            services.RegisterCutomServices();

            services.ConfigureResponseOptimisation();

            services.ConfigureModelValidation();

            AutoMapperConfig.Configure();

            services.AddValidatorsFromAssemblyContaining<Startup>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.InnerIISFix();

            app.UseAzureCors();

            app.UseResponseCompression();

            app.UseStaticFilesWithChaching();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
