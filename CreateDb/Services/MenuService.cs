using System;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace CreateDb.Services
{
    public interface IMenuForStaffService
    {
        public List<MenuEntity> GetMenu();

        public MenuEntity OneDish(string dishName);

        public void AddToMenu(List<MenuEntity> menuEntity);

        public void EditMenu(MenuEntity menuEntity);

        public void RemoveFromMenu(MenuEntity menuEntity);
    }

    public interface IMenuForClientsService
    {
        public List<MenuEntity> GetMenu();

        public MenuEntity OneDish(string dishName);
    }

    public class MenuService : IMenuForStaffService, IMenuForClientsService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public MenuService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<MenuEntity> GetMenu()
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var fullMenu = _context.Menus.ToList();
            //foreach(var m in fullMenu)
            //{
            //    Console.WriteLine($"{m.ProductName} - {m.Price}");
            //}
            return fullMenu;
        }

        public MenuEntity OneDish(string dishName)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var selectDish = _context.Menus.FirstOrDefault(m => m.ProductName == dishName);

            return selectDish;
        }

        public void AddToMenu(List<MenuEntity> menuEntity)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            foreach (var m in menuEntity)
            {
                _context.Menus.Add(m);
            }
            _context.SaveChanges();
        }

        public void EditMenu(MenuEntity menuEntity)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var editMenu = _context.Menus.FirstOrDefault(m => m.Id == menuEntity.Id);

            editMenu.ProductName = menuEntity.ProductName;
            editMenu.Price = menuEntity.Price;

            _context.SaveChanges();
        }

        public void RemoveFromMenu(MenuEntity menuEntity)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var removeDish = _context.Menus.FirstOrDefault(m => m.Id == menuEntity.Id);

            _context.Menus.Remove(removeDish);
            _context.SaveChanges();
        }


    }
}
