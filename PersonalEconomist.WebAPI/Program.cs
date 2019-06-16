using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalEconomist.Services.Extensions;

namespace PersonalEconomist.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args);
        }

        public static async void MainAsync(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            await EnsureDatabaseInitialized(host);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static async Task EnsureDatabaseInitialized(IWebHost host)
        {
            using (var serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                await DatabaseInitializer.EnsureDatabaseInitialized(serviceScope);
            }

            Task.Run(async () =>
            {
                using (IServiceScope serviceScope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
                {
                    await DatabaseInitializer.WarmUpContext(serviceScope);
                }
            });

        }
    }
}
