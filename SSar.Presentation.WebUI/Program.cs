using System;
using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace SSar.Presentation.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.Async( a => 
                        a.File(
                            "\\Logs\\ApplicationLog-.txt",
                            rollingInterval: RollingInterval.Hour,
                            buffered: true,
                            flushToDiskInterval: TimeSpan.FromSeconds(3)))
                    .CreateLogger();

                Log.Information("Hello world!");
                Log.CloseAndFlush();
            }
            catch(Exception e)
            {
                Debug.WriteLine("- Exception message: " + e.Message);
                throw;
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
