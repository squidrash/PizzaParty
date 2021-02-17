using System;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreateDb.Services
{
    //public interface IUserForStaffService
    //{
    //    public List<CustomerEntity> AllUsers();
    //    public CustomerEntity SelectUser(string Name, string LastName, int Id);
    //    public void RegistrationUser(List<CustomerEntity> customers);
    //    public void DeleteUser(List<CustomerEntity> customers);
    //    public void EditUser(CustomerEntity customer);
    //}

    //public interface IUserForCustomerService
    //{
    //    public CustomerEntity SelectUser(string Name, string LastName, int Id);
    //    public void EditUser(CustomerEntity customer);
    //}

    public interface IUserService
    {
        public List<CustomerEntity> AllUsers();

        public CustomerEntity SelectUserFromDb(string Name, string LastName, int Id);
        public CustomerEntity SelectUser(string Name, string LastName, string Phone);

        public void RegistrationUser(List<CustomerEntity> customers);
        public void DeleteUser(List<CustomerEntity> customers);
        public void EditUser(CustomerEntity customer);

    }

    public class UserService : IUserService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public UserService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public List<CustomerEntity> AllUsers()
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var users = _context.Customers
                .OrderBy(u => u.Name)
                .ThenBy(u => u.LastName)
                .ToList();
            return users;
        }

        public CustomerEntity SelectUserFromDb(string Name, string LastName, int Id)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var selectedUser = _context.Customers
                .Where(u => u.Name == Name && u.LastName == LastName && u.Id == Id)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Products)
                .ThenInclude(p => p.Dish)
                .Include(u => u.Addresses)
                .ThenInclude(a => a.Address)
                .FirstOrDefault();
            return selectedUser;
        }
        public CustomerEntity SelectUser(string Name, string LastName, string Phone)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var selectedUser = _context.Customers
                .Where(u => u.Name == Name && u.LastName == LastName && u.Phone == Phone)
                .FirstOrDefault();
            return selectedUser;
        }

        public void RegistrationUser(List<CustomerEntity> customers) 
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            foreach( var c in customers)
            {
                _context.Customers.Add(c);
            }
            _context.SaveChanges();
        }

        public void DeleteUser(List<CustomerEntity> customers)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            foreach(var c in customers)
            {
                _context.Customers.Remove(c);
            }
            _context.SaveChanges();
        }

        public void EditUser(CustomerEntity customer)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var infoCustomer = _context.Customers
                .FirstOrDefault(c => c.Id == customer.Id);

            infoCustomer.Name = customer.Name;
            infoCustomer.LastName = customer.LastName;
            infoCustomer.Phone = customer.Phone;
            infoCustomer.Discount = customer.Discount;

            _context.SaveChanges();
        }


        //public void AllUsers()
        //{
        //    using var scope = _scopeFactory.CreateScope();
        //    var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

        //    var users = _context.Customers
        //        .Include(u => u.Orders)
        //        .ThenInclude(o => o.Products)
        //        .ThenInclude(p => p.Dish)
        //        .ToList();
        //    foreach (var u in users)
        //    {
        //        Console.WriteLine($"{u.Name} {u.LastName}");
        //        foreach (var o in u.Orders)
        //        {
        //            Console.WriteLine($"{o.CreatTime} {o.Status}");
        //            foreach (var p in o.Products)
        //            {
        //                Console.WriteLine($"{p.Dish.ProductName}  {p.CountDish}");
        //            }
        //        }
        //    }

        //}
    }
}
