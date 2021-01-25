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

            //MenuEntity pizza = new MenuEntity { ProductName = "Пицца" };
            //MenuEntity coffee = new MenuEntity { ProductName = "Кофе" };
            //MenuEntity burger = new MenuEntity { ProductName = "Бургер" };
            //_context.Menus.AddRange(pizza, coffee, burger);

            MenuEntity sushi = new MenuEntity { ProductName = "Филадельфия" };
            MenuEntity sushi2 = new MenuEntity { ProductName = "Киото" };

            _context.Menus.AddRange(sushi, sushi2);
            _context.SaveChanges();
        }
        public void AddData(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            //UserEntity tom = new UserEntity { Name = "Tom", LastName = "Smit", Address = "Red square", Phone = "+79629999999", Discount = 20 };
            //_context.Users.Add(tom);

            //OrderEntity firstOrder = new OrderEntity
            //{
            //    CreatTime = DateTime.Now,
            //    UserOrder = tom,
            //    Status = "готовится"
            //};
            //_context.Orders.Add(firstOrder);

            //UserEntity bob = new UserEntity { Name = "Robert", LastName = "Holland", Address = "Stavropol", Phone = "+79620000000", Discount = 5 };
            //_context.Users.Add(bob);

            //OrderEntity secondOrder = new OrderEntity
            //{
            //    CreatTime = DateTime.Now,
            //    UserOrder = bob,
            //    Status = "готовится"
            //};
            //_context.Orders.Add(secondOrder);


            //_context.SaveChanges();
        }
        public void AddToBascet(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            //var user = _context.Users.FirstOrDefault(u => u.Id == 1);
            //var order = _context.Orders.FirstOrDefault(o => o.Id == 1);
            //var menu = _context.Menus.FirstOrDefault(o => o.Id == 1);
            //var menu2 = _context.Menus.FirstOrDefault(o => o.Id == 3);

            //OrderMenuEntity firstDish = new OrderMenuEntity() {Dish = menu, Order = order, CountDish = 1 };
            //OrderMenuEntity secondDish = new OrderMenuEntity() { Dish = menu2, Order = order, CountDish = 5 };

            //_context.Bascets.AddRange(firstDish, secondDish);

            var user = _context.Users.FirstOrDefault(u => u.Name == "Robert");

            var orders = _context.Orders.FirstOrDefault(o => o.UserEntityId == user.Id);
            var menu = _context.Menus.FirstOrDefault(o => o.ProductName == "Филадельфия");
            var menu2 = _context.Menus.FirstOrDefault(o => o.ProductName == "Киото");

            OrderMenuEntity firstDish = new OrderMenuEntity() {Dish = menu, Order = orders, CountDish = 2 };
            OrderMenuEntity secondDish = new OrderMenuEntity() { Dish = menu2, Order = orders, CountDish = 3 };
            _context.Bascets.AddRange(firstDish, secondDish);


            //Console.WriteLine($"{firstDish.MenuEntityId} { firstDish.OrderEntityId}");
            //Console.WriteLine($"{secondDish.MenuEntityId},  {secondDish.OrderEntityId}");


            _context.SaveChanges();
        }

        public void AddAddress(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var user = _context.Users.
                Where(u => u.Name == "Robert").FirstOrDefault();

            AddressEntity addAddress = new AddressEntity() { City = "Ставрополь", Street = "Ленина", NumberOfBuild = "1", UserAddress = user };

            _context.Addresses.Add(addAddress);
            _context.SaveChanges();

        }
    }
}
