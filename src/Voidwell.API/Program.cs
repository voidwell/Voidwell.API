using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Voidwell.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
