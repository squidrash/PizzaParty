using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreateDb.Services;
using CreateDb.Services.CustomerActions;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using CreateDb.TestDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreateDb
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ICustomerActionsService _customerActions;
        private readonly ITestService _testService;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory, ICustomerActionsService customerActions, ITestService testService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _customerActions = customerActions;
            _testService = testService;

            ApplyMigrations();

            _customerActions.GetFullMenu();
            Console.WriteLine("Попытка что то сделать");
            _testService.TestAddress();

            //TestAllOrders();

            //TestChangeOrderStatus();

            //TestCreateOrder();

            //изменение
            //var fullMenu = _staffService.GetMenu();
            //var dishName = "Пицца";
            //var oneDish = _staffService.OneDish(dishName);
            //Console.WriteLine(oneDish.ProductName);
            //oneDish.ProductName = "Пицца 4 сыра";
            //oneDish.Price = 350;
            //_staffService.EditMenu(oneDish);
            //Console.WriteLine(oneDish.ProductName);


            //удаление
            //var dishName = "Пицца 4 сыра";
            //var oneDish = _staffService.OneDish(dishName);
            //_staffService.RemoveFromMenu(oneDish);

            //добавление
            //var firstDish = new MenuEntity() { ProductName = "Пицца", Price = 350 };
            //var seconddish = new MenuEntity() { ProductName = "Паста", Price = 400 };
            //List<MenuEntity> dishes = new List<MenuEntity>();
            //dishes.Add(firstDish);
            //dishes.Add(seconddish);
            //_staffService.AddToMenu(dishes);

        }

        

        private void ApplyMigrations()
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
            _context.Database.Migrate();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
