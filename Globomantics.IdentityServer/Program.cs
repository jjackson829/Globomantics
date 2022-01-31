using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Globomantics.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((contex, provider, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(contex.Configuration) // minimum levels defined per project in json files 
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Seq("http://globoseq");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
