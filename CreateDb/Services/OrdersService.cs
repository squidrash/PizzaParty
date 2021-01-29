using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services
{
    public interface IOrdersForStaffService
    {
        public OrderEntity ChangeOrderStatus(OrderEntity order, string orderStatus);

        public List<OrderEntity> AllOrders(CustomerEntity customer = null);

        public OrderEntity OrdersOfOneCustomer(CustomerEntity customer = null, string orderStatus = null);

    }

    public interface IOrdersForCustomerService
    {
        public OrderEntity CreateOrder(CustomerEntity customer = null);

        public OrderEntity OrdersOfOneCustomer(CustomerEntity customer = null, string orderStatus = null);
    }
        
    public class OrdersService : IOrdersForCustomerService, IOrdersForStaffService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public OrdersService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        readonly Dictionary<string, Status> OrderStatuses = new Dictionary<string, Status>
        {
            {"Новый", Status.New },
            {"Готовится", Status.Preparing },
            {"В пути", Status.OnTheWay},
            {"Доставлен", Status.Delivered},
            {"Отменен", Status.Cancelled}
        };

        //ошибка в SaveChanges
        public OrderEntity CreateOrder(CustomerEntity customer = null) 
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            OrderEntity order = new OrderEntity
            {
                CreatTime = DateTime.Now,
                CustomerOrder = customer,
                Status = Status.New
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        //работает
        //продумать проверку string OrderStatus
        public OrderEntity ChangeOrderStatus(OrderEntity order, string orderStatus)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var changeStatus = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            changeStatus.Status = OrderStatuses[orderStatus];
            _context.SaveChanges();
            return changeStatus;
        }

        public List<OrderEntity> AllOrders(CustomerEntity customer = null)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
            List<OrderEntity> orders;
            if(customer != null)
            {
                orders = customer.Orders.ToList();
            }
            else
            {
                orders = _context.Orders.ToList();
            }
            return orders;
        }

        //изменить принцип действия
        //и назнание
        public OrderEntity OrdersOfOneCustomer(CustomerEntity customer = null, string orderStatus = null) 
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            OrderEntity order = null;
            if(customer != null)
            {
                 order = customer.Orders
                .FirstOrDefault();
            }
            
            return order;
        }

    }
}
