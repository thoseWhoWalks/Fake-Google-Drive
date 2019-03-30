using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Api.Configuration
{
    public static class AppPipelineConfigurationExtention
    {
        public static void UseAzureCors(this IApplicationBuilder app)
        {
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins(
                  "http://localhost",
                 "http://localhost:5001",
                 "http://localhost:4200",
                 "http://fakegoogledrive.eastus.cloudapp.azure.com:5000",
                 "http://fakegoogledrive.eastus.cloudapp.azure.com:5001",
                 "http://fakegoogledrive.eastus.cloudapp.azure.com")
                 .AllowAnyMethod().AllowAnyHeader().AllowCredentials()
              );
        }

        public static void InnerIISFix(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });
        }

        public static void UseStaticFilesWithChaching(this IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=1600");
                }
            });

        }
    }
}
