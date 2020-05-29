using System;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProjectEulerWebApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureLogging((context, logging) =>
                                         {
                                             logging.ClearProviders();
                                             logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                                             logging.AddDebug();
                                             logging.AddConsole();
                                         })
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}