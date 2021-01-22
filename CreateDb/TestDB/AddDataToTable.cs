using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;

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
            _context.Menu.AddRange(pizza, coffee, burger);
            _context.SaveChanges();
        }
        public void AddData(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            UserEntity tom = new UserEntity { Name = "Tom", LastName = "Smit", Address = "Red square", Phone = "+79629999999", Discount = 20 };
            _context.Users.Add(tom);

            OrderEntity firstOrder = new OrderEntity
            {
                CreatTime = DateTime.Now,
                UserOrder = tom,
                Status = "готовится"
            };
            _context.Orders.Add(firstOrder);


            _context.SaveChanges();
        }
        public void AddToBascet(IServiceScopeFactory _scopeFactory)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var user = _context.Users.FirstOrDefault(u => u.Id == 1);
            var order = _context.Orders.FirstOrDefault(o => o.Id == 1);
            var menu = _context.Menu.FirstOrDefault(o => o.Id == 1);
            var menu2 = _context.Menu.FirstOrDefault(o => o.Id == 3);

            //Console.WriteLine($"{user.Name}, {order.CreatTime.TimeOfDay}, {menu.ProductName}");

            BascetEntity firstDish = new BascetEntity() { Dish = menu, Order = order };
            BascetEntity secondDish = new BascetEntity() { Dish = menu2, Order = order };

            _context.Bascet.AddRange(firstDish, secondDish);

            //Console.WriteLine($"{firstDish.MenuEntityId} { firstDish.OrderEntityId}");
            //Console.WriteLine($"{secondDish.MenuEntityId},  {secondDish.OrderEntityId}");


            _context.SaveChanges();
        }
    }
}
