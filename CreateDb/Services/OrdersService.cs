using System;
using CreateDb.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services
{
    public interface IOrdersForStaffService
    {
        public void ChangeOrderStatus()
        {

        }
        public void AllOrders()
        {

        }
        public void OrdersOfOneCustomer()
        {

        }
    }

    public interface IOrdersForCustomerService
    {
        public void CreateOrder()
        {

        }
        
        public void OrdersOfOneCustomer()
        {

        }
    }
        
    public class OrdersService : IOrdersForCustomerService, IOrdersForStaffService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public OrdersService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void CreateOrder(OrdersService order)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();


        }
        public void ChangeOrderStatus()
        {

        }
        public void AllOrders()
        {

        }
        public void OrdersOfOneCustomer()
        {

        }
    }
}
