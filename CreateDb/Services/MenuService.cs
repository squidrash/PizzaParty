using System;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services
{
    public interface IMenuService
    {
        public System.Collections.Generic.List<Storage.Models.MenuEntity> GetMenu();

        public void AddToMenu(MenuEntity[] menuEntity);

        public void EditMenu();
        
    }
    public class MenuService : IMenuService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public MenuService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public System.Collections.Generic.List<Storage.Models.MenuEntity> GetMenu()
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var fullMenu = _context.Menus.ToList();
            foreach(var m in fullMenu)
            {
                Console.WriteLine($"{m.ProductName} - {m.Price}");
            }
            return fullMenu;
        }
        public void AddToMenu(MenuEntity[] menuEntity)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            foreach (var m in menuEntity)
            {
                _context.Menus.Add(m);
            }
            _context.SaveChanges();
        }
        public void EditMenu()
        {

        }
    }
}
