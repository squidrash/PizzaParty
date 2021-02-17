using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services
{
    //public interface IAddressForStaffService
    //{
    //    public void CreateDeliveryAddress(string city, string street, string numberOfBuild,
    //        int numberOfEntrance = 0, int apartment = 0);

    //    public AddressEntity GetDeliveryAddress(string city, string street, string numberOfBuild,
    //        int numberOfEntrance = 0, int apartment = 0);

    //    public void EditDeliveryAddress(AddressEntity address);

    //    public void RemoveDeliveryAddress(AddressEntity address);
    //}
    //public interface IAddressForCustomerService
    //{
    //    public void CreateDeliveryAddress(string city, string street, string numberOfBuild,
    //        int numberOfEntrance = 0, int apartment = 0);

    //    public AddressEntity GetDeliveryAddress(string city, string street, string numberOfBuild,
    //        int numberOfEntrance = 0, int apartment = 0);

    //    public void EditDeliveryAddress(AddressEntity address);
    //}
    public interface IAddressesService
    {
        public void CreateDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0);
        public void CreateDeliveryAddress(AddressEntity address);
        public void EditDeliveryAddress(AddressEntity address);
        public AddressEntity GetDeliveryAddress(AddressEntity address);
        public void RemoveDeliveryAddress(AddressEntity address);

    }

    public class AddressesService : IAddressesService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public AddressesService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        //оставить так или сделать принмаемым параметром объект AddressEntity???
        public void CreateDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            AddressEntity address = new AddressEntity
            {
                City = city,
                Street = street,
                NumberOfBuild = numberOfBuild,
                NumberOfEntrance = numberOfEntrance,
                Apartment = apartment
            };
            var createAddress = _context.Addresses.Add(address);
            _context.SaveChanges();
        }
        public void CreateDeliveryAddress(AddressEntity address)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var createAddress = _context.Addresses.Add(address);
            _context.SaveChanges();
        }


        public void EditDeliveryAddress(AddressEntity address)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var changeableAddress = _context.Addresses
                .Where(a => a.Id == address.Id)
                .FirstOrDefault();

            changeableAddress.City = address.City;
            changeableAddress.Street = address.Street;
            changeableAddress.NumberOfBuild = address.NumberOfBuild;
            changeableAddress.NumberOfEntrance = address.NumberOfEntrance;
            changeableAddress.Apartment = address.Apartment;

            _context.SaveChanges();
        }
        public AddressEntity GetDeliveryAddress(AddressEntity deliveryAddress)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var address = _context.Addresses
                .Where(a => a.City == deliveryAddress.City)
                .Where(a => a.Street == deliveryAddress.Street)
                .Where(a => a.NumberOfBuild == deliveryAddress.NumberOfBuild);
            if(deliveryAddress.NumberOfEntrance != null)
            {
                address = address
                    .Where(a => a.NumberOfEntrance == deliveryAddress.NumberOfEntrance);
            }
            if(deliveryAddress.Apartment != null)
            {
                address = address
                    .Where(a => a.Apartment == deliveryAddress.Apartment);
            }
            var selectAddress = address.FirstOrDefault();
                
            return selectAddress;
        }
        public void RemoveDeliveryAddress(AddressEntity address)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }
    }
}
