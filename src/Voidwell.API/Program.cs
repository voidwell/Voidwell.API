using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Voidwell.Logging;

namespace Voidwell.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5000")
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();

                    var useGelf = context.Configuration.GetValue("UseGelfLogging", false);

                    builder.SetMinimumLevel(LogLevel.Information);

                    builder.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
                    builder.AddFilter("Microsoft.AspNetCore.Mvc", LogLevel.Error);


                    if (useGelf && !context.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddGelf(options =>
                        {
                            options.LogSource = "Voidwell.API";
                        });
                    }
                    else
                    {
                        builder.AddConsole();
                        builder.AddDebug();
                    }
                })
                .Build();
    }
}
