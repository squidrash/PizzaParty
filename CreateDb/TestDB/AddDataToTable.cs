using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CreateDb.TestDB
{
    public class AddDataToTable
    {
        public void CreateMenu(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            MenuEntity pizza = new MenuEntity { ProductName = "Пицца" };
            MenuEntity coffee = new MenuEntity { ProductName = "Кофе" };
            MenuEntity burger = new MenuEntity { ProductName = "Бургер" };
            _context.Menus.AddRange(pizza, coffee, burger);

            MenuEntity sushi = new MenuEntity { ProductName = "Филадельфия" };
            MenuEntity sushi2 = new MenuEntity { ProductName = "Киото" };

            _context.Menus.AddRange(sushi, sushi2);
            _context.SaveChanges();
        }
        public void AddCustomerAndOrders(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            CustomerEntity tom = new CustomerEntity { Name = "Tom", LastName = "Smit", Phone = "+79629999999", Discount = 20 };
            _context.Customers.Add(tom);

            OrderEntity firstOrder = new OrderEntity
            {
                CreatTime = DateTime.Now,
                Customer = tom,
                Status = Status.New
            };
            _context.Orders.Add(firstOrder);

            CustomerEntity bob = new CustomerEntity { Name = "Robert", LastName = "Holland", Phone = "+79620000000", Discount = 5 };
            _context.Customers.Add(bob);

            OrderEntity secondOrder = new OrderEntity
            {
                CreatTime = DateTime.Now,
                Customer = bob,
                Status = Status.New
            };
            _context.Orders.Add(secondOrder);


            _context.SaveChanges();
        }
        public void AddToBascet(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var user = _context.Customers.FirstOrDefault(u => u.Id == 1);
            var order = _context.Orders.FirstOrDefault(o => o.Id == 1);
            var menu = _context.Menus.FirstOrDefault(o => o.Id == 1);
            var menu2 = _context.Menus.FirstOrDefault(o => o.Id == 3);

            OrderMenuEntity firstDish = new OrderMenuEntity() { Dish = menu, Order = order, CountDish = 1 };
            OrderMenuEntity secondDish = new OrderMenuEntity() { Dish = menu2, Order = order, CountDish = 5 };

            _context.OrderMenuEntities.AddRange(firstDish, secondDish);

            var user11 = _context.Customers.FirstOrDefault(u => u.Name == "Robert");

            var orders11 = _context.Orders.FirstOrDefault(o => o.CustomerEntityId == user11.Id);
            var menu11 = _context.Menus.FirstOrDefault(o => o.ProductName == "Филадельфия");
            var menu211 = _context.Menus.FirstOrDefault(o => o.ProductName == "Киото");

            OrderMenuEntity firstDish11 = new OrderMenuEntity() { Dish = menu11, Order = orders11, CountDish = 2 };
            OrderMenuEntity secondDish11 = new OrderMenuEntity() { Dish = menu211, Order = orders11, CountDish = 3 };
            _context.OrderMenuEntities.AddRange(firstDish11, secondDish11);


            //Console.WriteLine($"{firstDish.MenuEntityId} { firstDish.OrderEntityId}");
            //Console.WriteLine($"{secondDish.MenuEntityId},  {secondDish.OrderEntityId}");


            _context.SaveChanges();
        }

        public void AddAddress(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var user = _context.Customers.
                Where(u => u.Name == "Robert").FirstOrDefault();

            AddressEntity addAddress = new AddressEntity() { City = "Ставрополь", Street = "Ленина", NumberOfBuild = "1", Customer = user };

            _context.Addresses.Add(addAddress);
            _context.SaveChanges();

        }
    }
}
