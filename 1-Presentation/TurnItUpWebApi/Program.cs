using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace TurnItUpWebApi
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                WebHostExtensions.Run(Program.BuildWebHost(args));
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return SerilogWebHostBuilderExtensions.UseSerilog(WebHostBuilderExtensions.UseStartup<Startup>(WebHost.CreateDefaultBuilder(args)), (ILogger)null, false).Build();
        }
    }
}
