using FinaltestLibrary.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinaltestLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataAccess dataAccess = new DataAccess();
            Context context = new Context();
            //dataAccess.RecreateDatabase();
            //dataAccess.SeedDatabase();
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
