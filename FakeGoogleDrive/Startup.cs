using FGD.Api.Configuration;
using FGD.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.SetUpOptions();

            services.RegisterDatabaseContext();

            services.ConfigureCors();

            services.ConfigureAuthentication();

            services.RegisterCutomServices();

            services.ConfigureResponseOptimisation();

            services.ConfigureModelValidation();

            AutoMapperConfig.Configure();

            services.AddMvc()
                .AddFluentValidation(op => op.RegisterValidatorsFromAssembly(GetType().Assembly))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.InnerIISFix();

            app.UseAzureCors();

            app.UseResponseCompression();

            app.UseStaticFilesWithChaching();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
