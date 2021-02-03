using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services
{
    public interface IAddressServiceForStaff
    {

    }
    public interface IAddressServiceForCustomer
    {

    }
    public class AddressesService : IAddressServiceForCustomer ,IAddressServiceForStaff
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public AddressesService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void CreateDeliveryAddress()
        {

        }
        public void EditDeliveryAddress()
        {

        }
        public AddressEntity GetDeliveryAddress()
        {
            AddressEntity add = new AddressEntity();
            return add;
        }
        public void RemoveDeliveryAddress()
        {

        }
    }
}
