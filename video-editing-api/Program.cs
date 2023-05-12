using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace video_editing_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseKestrel(options =>
                        {
                            // Heroku sets the PORT environment variable, and .NET uses the HTTP_PORT variable.
                            string port = Environment.GetEnvironmentVariable("PORT") ?? "44394";
                            Environment.SetEnvironmentVariable("ASPNETCORE_URLS", $"http://*:{port}");

                            options.Listen(IPAddress.Any, int.Parse(port));
                        });
                });
    }
}