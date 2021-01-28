using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateDb.Services;
using CreateDb.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreateDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    string pgConnection = hostContext.Configuration.GetConnectionString("PgConnection");

                    services.AddDbContext<PizzaDbContext>(options => options.UseNpgsql(pgConnection));
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IMenuForStaffService, MenuService>();
                    services.AddSingleton<IMenuForClientsService, MenuService>();
                });
    }
}
