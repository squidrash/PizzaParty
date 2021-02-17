using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services
{
    //public interface IOrdersForStaffService
    //{
    //    public OrderEntity ChangeOrderStatus(OrderEntity order, string orderStatus);

    //    public List<OrderEntity> AllOrders(CustomerEntity customer = null);

    //    //public OrderEntity OneOrderOfOneCustomer(CustomerEntity customer, string orderStatus = null);

    //}

    //public interface IOrdersForCustomerService
    //{
    //    public void CreateOrder(CustomerEntity customer = null);
    //    public List<OrderEntity> AllOrders(CustomerEntity customer);

    //    //public OrderEntity OneOrderOfOneCustomer(CustomerEntity customer, string orderStatus = null);
    //}

    public interface IOrdersService
    {
        public OrderEntity CreateOrder(CustomerEntity customer = null);
        public OrderEntity ChangeOrderStatus(OrderEntity order, string orderStatus);
        public List<OrderEntity> AllOrders(CustomerEntity customer = null);
    }
        
    public class OrdersService : IOrdersService
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

        //работает
        public OrderEntity CreateOrder(CustomerEntity customer = null) 
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            OrderEntity order = new OrderEntity();

            //можно сократить до
            //order.CreatTime = DateTime.Now;
            //order.Status = Status.New;
            //if (customer != null)
            //{ 
            //    order.CustomerEntityId = customer.Id;
            //}
            if (customer != null)
            {
                order.CreatTime = DateTime.Now;
                order.Status = Status.New;
                order.CustomerEntityId = customer.Id;
            }
            else
            {
                order.CreatTime = DateTime.Now;
                order.Status = Status.New;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        //работает
        //продумать проверку string OrderStatus
        //убрать return
        public OrderEntity ChangeOrderStatus(OrderEntity order, string orderStatus)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var changeStatus = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            changeStatus.Status = OrderStatuses[orderStatus];
            _context.SaveChanges();
            return changeStatus;
        }

        //Работает
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
                orders = _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Products)
                    .ThenInclude(p => p.Dish)
                    .ToList();
            }
            return orders;
        }

        //изменить принцип действия
        //и назнание
        //public OrderEntity OneOrderOfOneCustomer(CustomerEntity customer, string orderStatus = null)
        //{
        //    using var scope = _scopeFactory.CreateScope();
        //    var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

        //    OrderEntity order = null;
        //    if (customer != null)
        //    {
        //        order = customer.Orders
        //            .Where(o => o.Status == OrderStatuses[orderStatus])
        //       .FirstOrDefault();
        //    }

        //    return order;
        //}
    }
}
