using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreateDb.Services;
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

        private readonly IMenuForStaffService _menuStaffService;
        private readonly IMenuForClientsService _menuClientsService;

        private readonly IOrdersForCustomerService _ordersCustomerService;
        private readonly IOrdersForStaffService _ordersStaffService;

        private readonly IUserForCustomerService _userForCustomerService;
        private readonly IUserForStaffService _userForStaffService;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory,
            IMenuForStaffService menuStaffService, IMenuForClientsService menuClientsService,
            IOrdersForCustomerService ordersCustomerService, IOrdersForStaffService ordersStaffService,
            IUserForCustomerService userForCustomerService, IUserForStaffService userForStaffService)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            _menuStaffService = menuStaffService;
            _menuClientsService = menuClientsService;

            _ordersCustomerService = ordersCustomerService;
            _ordersStaffService = ordersStaffService;

            _userForCustomerService = userForCustomerService;
            _userForStaffService = userForStaffService;

            ApplyMigrations();

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

        private void InitialData()
        {
            AddDataToTable testDB = new AddDataToTable();
            testDB.CreateMenu(_scopeFactory);
            testDB.AddCustomerAndOrders(_scopeFactory);
            testDB.AddToBascet(_scopeFactory);
            testDB.AddAddress(_scopeFactory);
        }
        private void TestAllOrders()// работает
        {
            Console.WriteLine("метод с передачей клиента");

            var user = _userForStaffService.SelectUser("Tom", "Smit");
            Console.WriteLine($"{user.Name} {user.LastName}");
            Console.WriteLine($"Номер: {user.Phone} скидка: {user.Discount}");
            var orders = _ordersStaffService.AllOrders(user);

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.CreatTime} - {order.CustomerEntityId} - status {order.Status} ");
            }

            Console.WriteLine("метод без клиента");

            var ord = _ordersStaffService.AllOrders();
            foreach(var o in ord)
            {
                Console.WriteLine($"{o.CreatTime} - {o.CustomerEntityId} - status {o.Status} - {o.CustomerOrder.Name} {o.CustomerOrder.LastName}");
            }
        }
        private void TestCreateOrder()// ошибка в SaveChanges
        {
            var user = _userForStaffService.SelectUser("Tom", "Smit");
            Console.WriteLine($"{user.Name} {user.LastName}");
            Console.WriteLine($"{user.Phone} {user.Discount}");

            //var order = _ordersCustomerService.CreateOrder(user);
            //Console.WriteLine("метод с передачей клиента");
            //Console.WriteLine($"{order.CreatTime} - {order.CustomerEntityId} - status {order.Status}");
            //var order1 = _ordersCustomerService.CreateOrder();
            //Console.WriteLine("метод без передачи клиента");
            //Console.WriteLine($"{order1.CreatTime} - {order1.CustomerEntityId} - status {order1.Status}");
            _ordersCustomerService.CreateOrder(user);
            
             _ordersCustomerService.CreateOrder();
            
        }
        private void TestChangeOrderStatus()// работает
        {
            var user = _userForStaffService.SelectUser("Tom", "Smit");
            Console.WriteLine($"{user.Name} {user.LastName}");
            Console.WriteLine($"Номер: {user.Phone} скидка: {user.Discount}");
            var orders = user.Orders;
            foreach (var o in orders)
            {
                Console.WriteLine($"Время создания заказа: {o.CreatTime.TimeOfDay}б  статус заказа: {o.Status}");
                var products = o.Products;
                foreach (var p in products)
                {
                    Console.WriteLine($"Наименование блюда: {p.Dish.ProductName}, количество - {p.CountDish}");
                }
            }

            var order = user.Orders
                .Where(o => o.Status == Status.Preparing)
                .FirstOrDefault();
            var orderStatus = "Новый";
            Console.WriteLine($"статус до изменения {order.Status}");
            var result = _ordersStaffService.ChangeOrderStatus(order, orderStatus);
            Console.WriteLine($"статус после изменения {result.Status}");
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
