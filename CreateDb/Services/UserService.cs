﻿using System;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CreateDb.Services
{
    public interface IUserForStaffService
    {
        //public void AllUsers();
        //public void SelectUser();
        //public void RegistrationUser();
        //public void DeleteUser();
        //public void EditUser();
    }

    public interface IUserForCustomerService
    {
        //public void SelectUser();
        //public void EditUser();
    }

    public class UserService : IUserForStaffService, IUserForCustomerService
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
                .OrderBy(u => u)
                .ToList();
            return users;
        }

        public CustomerEntity SelectUser(string Name, string LastName)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var selectedUser = _context.Customers.FirstOrDefault(u => u.Name == Name && u.LastName == LastName);
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
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == customer.Id);
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