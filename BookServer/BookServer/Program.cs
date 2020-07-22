using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BookServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            // The default ASP.NET Core project templates adds the following logging providers:
            // Console, Debug, EvenSource, and EventLog (Windows only)
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
