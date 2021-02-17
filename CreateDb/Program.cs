using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateDb.Services;
using CreateDb.Services.CustomerActions;
using CreateDb.Storage;
using CreateDb.TestDB;
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

                    //services.AddSingleton<IMenuForStaffService, MenuService>();
                    //services.AddSingleton<IMenuForClientsService, MenuService>();
                    services.AddSingleton<IMenuService, MenuService>();

                    //services.AddSingleton<IUserForCustomerService, UserService>();
                    //services.AddSingleton<IUserForStaffService, UserService>();
                    services.AddSingleton<IUserService, UserService>();

                    //services.AddSingleton<IOrdersForCustomerService, OrdersService>();
                    //services.AddSingleton<IOrdersForStaffService, OrdersService>();
                    services.AddSingleton<IOrdersService, OrdersService>();

                    //services.AddSingleton<IAddressForCustomerService, AddressesService>();
                    //services.AddSingleton<IAddressForStaffService, AddressesService>();
                    services.AddSingleton<IAddressesService, AddressesService>();

                    services.AddSingleton<IOrderMenuService, OrderMenuService>();

                    services.AddSingleton<IAddressOrderService, AddressOrderService>();

                    services.AddSingleton<ICustomerAddressService, CustomerAddressService>();

                    services.AddSingleton<ICustomerActionsService, CustomerActionsService>();

                    services.AddSingleton<ITestService, TestService>();

                });
    }
}
