using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FGD.Data;
using FGD.Data.Initializer;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FakeGoogleDrive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var contexts = services.GetRequiredService<FakeGoogleDriveContext>();
                    contexts.Database.EnsureCreated();
                    DevInitInitializer.Initialize(contexts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while seeding the database. " + ex.Message);
                }
            }

            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args) 
                .UseStartup<Startup>();
    }
}
