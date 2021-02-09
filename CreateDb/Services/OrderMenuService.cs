using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace CreateDb.Services
{
    public interface IOrderMenuService
    {
        public void OrderMenu(OrderEntity order, MenuEntity dish, int count);
    }

    public class OrderMenuService :IOrderMenuService
    {
        private IServiceScopeFactory _scopeFactory;
        public OrderMenuService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void OrderMenu(OrderEntity order, MenuEntity dish, int count)
        {
            var orderMenu = new OrderMenuEntity { OrderEntityId = order.Id, MenuEntityId = dish.Id, CountDish = count };

            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            _context.OrderMenuEntities.Add(orderMenu);
            _context.SaveChanges();
        }
    }
}
