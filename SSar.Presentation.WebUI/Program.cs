using System;
using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.IO;

namespace SSar.Presentation.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string logPath = AppDomain.CurrentDomain.BaseDirectory +  // Deep in bin/... folder and
                "..\\..\\..\\Logs\\ApplicationLog -.txt";             // back up to project root (hopefully)

            Debug.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()                                 // Application log threshold
                .MinimumLevel.Override(
                    "Microsoft", LogEventLevel.Debug)               // AspNetCore log threshold
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Async( p=> 
                    p.File(
                        path: logPath,                                // Project root folder
                        rollingInterval: RollingInterval.Hour,
                        buffered: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(3),
                        fileSizeLimitBytes: 52428800,                 // 50 MB
                        retainedFileCountLimit: 96))                  // Four days, max ~4.8GB 
                .CreateLogger();

                // Buffering may cause loss of last few log items in a hard server crash.
                // If needed, temporarily remove buffering and flush interval for debugging.

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
