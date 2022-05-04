using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebApplication11.Persistence;

[assembly: InternalsVisibleToAttribute("TestProject1")]

namespace WebApplication11
{
    public class Program
    {
        public static AppDbContext Context { get; set; }
        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AppDbContext>();
            await SeedData.Seed(context);


            await host.RunAsync();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                });


    }




}



