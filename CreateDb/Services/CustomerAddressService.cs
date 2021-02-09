using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace CreateDb.Services
{
    public interface ICustomerAddressService
    {
        public void CustomerAddress(CustomerEntity customer, AddressEntity address);
    }
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public CustomerAddressService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public void CustomerAddress(CustomerEntity customer, AddressEntity address)
        {
            var customerAddress = new CustomerAddressEntity { AddressEntityId = address.Id, CustomerEntityId = customer.Id };

            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            _context.CustomerAddressEntities.Add(customerAddress);
            _context.SaveChanges();
        }
    }
}
