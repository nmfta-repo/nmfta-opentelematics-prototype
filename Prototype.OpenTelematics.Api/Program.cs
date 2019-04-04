using AspNetCoreRateLimit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Prototype.OpenTelematics.Api
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            var webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                // get the Client Policy Store instance
                var clientPolicyStore = scope.ServiceProvider.GetRequiredService<IClientPolicyStore>();

                // seed client data from app settings                
                clientPolicyStore.SeedAsync();
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
