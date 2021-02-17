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
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var checkCuctomerAddress = _context.CustomerAddressEntities
                .Where(c => c.CustomerEntityId == customer.Id && c.AddressEntityId == address.Id)
                .FirstOrDefault();

            if(checkCuctomerAddress == null)
            {
                var customerAddress = new CustomerAddressEntity { CustomerEntityId = customer.Id, AddressEntityId = address.Id, };
                _context.CustomerAddressEntities.Add(customerAddress);
                _context.SaveChanges();
            }
        }
    }
}
