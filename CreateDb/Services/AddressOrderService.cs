using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace CreateDb.Services
{
    public interface IAddressOrderService
    {
        public void AddressOrder(AddressEntity address, OrderEntity order);
    }

    public class AddressOrderService :IAddressOrderService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public AddressOrderService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void AddressOrder(AddressEntity address, OrderEntity order)
        {
            var addressOrder = new AddressOrderEntity { AddressEntityId = address.Id, OrderEntityId = order.Id };

            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            _context.AddressOrderEntities.Add(addressOrder);
            _context.SaveChanges();
        }
    }
}
