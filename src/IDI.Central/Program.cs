using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IDI.Central
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();

        public static void Main(string[] args)
        {
            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    //.UseUrls("http://*", "https://*")
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .UseApplicationInsights()
            //    .Build();

            //host.Run();
            BuildWebHost(args).Run();
        }

        
    }
}
