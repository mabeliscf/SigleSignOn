using Microsoft.AspNetCore;

namespace QRA.API
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
                    webBuilder.UseStartup<Startup>();
                });

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging => {
                logging.SetMinimumLevel(LogLevel.Error);
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
            }).UseStartup<Startup>();


    }
}

