using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FollowAlongLearnAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        //TODO 0.0.a This is where the startup will be called from.  
        //TODO 0.0.b Let's also take this time to edit the launchSettings.json file, you'll find in that file what should be in there.
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
