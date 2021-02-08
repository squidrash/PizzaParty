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
        public void CreateDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0);

        public AddressEntity GetDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0);

        public void EditDeliveryAddress(AddressEntity address);

        public void RemoveDeliveryAddress(AddressEntity address);
    }
    public interface IAddressServiceForCustomer
    {
        public void CreateDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0);

        public AddressEntity GetDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0);

        public void EditDeliveryAddress(AddressEntity address);
    }

    public class AddressesService : IAddressServiceForCustomer ,IAddressServiceForStaff
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
        public AddressEntity GetDeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

            var address = _context.Addresses
                .Where(a => a.City == city
                && a.Street == street
                && a.NumberOfBuild == numberOfBuild
                && a.NumberOfEntrance == numberOfEntrance
                && a.Apartment == apartment)
                .Include(a => a.Customer)
                //нужно ли это все загружать?
                //и если нужно то что??
                .Include(a => a.AddressOrder.Order)
                .ThenInclude(o => o.Products)
                .FirstOrDefault();

            //var address = _context.Addresses
            //    .Where(a => a.City == city)
            //    .Where(a => a.Street == street)
            //    .Where(a => a.NumberOfBuild == numberOfBuild)
            //    .Where(a => a.NumberOfEntrance == numberOfEntrance)
            //    .Where(a => a.Apartment == apartment)
            //    .FirstOrDefault();

            return address;
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
