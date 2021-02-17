using System;
using System.Collections.Generic;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services.CustomerActions
{
    public interface ICustomerActionsService
    {
        public List<MenuEntity> GetFullMenu();
    }
    public class CustomerActionsService : ICustomerActionsService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMenuService _menuService;
        private readonly IAddressesService _addressesService;
        private readonly IUserService _userService;
        private readonly ICustomerAddressService _customerAddressService;
        private readonly IOrdersService _ordersService;
        private readonly IOrderMenuService _orderMenuService;
        private readonly IAddressOrderService _addressOrderService;

        //вместо _customer должен быть залогиненный пользователь
        private CustomerEntity _customer;

        public CustomerActionsService(IServiceScopeFactory scopeFactory, IMenuService menuService,
             IAddressesService addressesService, IUserService userService,
             ICustomerAddressService customerAddressService,
             IOrdersService ordersService, IAddressOrderService addressOrderService,
             IOrderMenuService orderMenuService, CustomerEntity customer = null)
        {
            _scopeFactory = scopeFactory;
            _menuService = menuService;
            _customer = customer;
            _addressesService = addressesService;
            _userService = userService;
            _customerAddressService = customerAddressService;
            _ordersService = ordersService;
            _orderMenuService = orderMenuService;
            _addressOrderService = addressOrderService;
        }

        public List<MenuEntity> GetFullMenu()
        {
            var fullMenu = _menuService.GetMenu();
            foreach (var m in fullMenu)
            {
                Console.WriteLine($"{m.ProductName} - {m.Price}");
            }
            return fullMenu;
        }

        public AddressEntity DeliveryAddress(string city, string street, string numberOfBuild,
            int numberOfEntrance = 0, int apartment = 0, CustomerEntity customer = null)// customer - залогиненный пользователь
        {
            var deliveryAddress = new AddressEntity
            {
                City = city,
                Street = street,
                NumberOfBuild = numberOfBuild,
                NumberOfEntrance = numberOfEntrance,
                Apartment = apartment
            };

            var address = _addressesService.GetDeliveryAddress(deliveryAddress);
            if(address == null)
            {
                _addressesService.CreateDeliveryAddress(deliveryAddress);
            }

            if (customer != null )
            {
                address = _addressesService.GetDeliveryAddress(deliveryAddress);
                _customerAddressService.CustomerAddress(customer, address);
            }
            return address;
        }
        
        public void CreateOrder(List<MenuEntity> dishes, List<int> quantity, CustomerEntity customer = null, AddressEntity address = null )
        {
            var order = _ordersService.CreateOrder(customer);
            for(int i = 0; i < dishes.Count; i++)
            {
                _orderMenuService.OrderMenu(order, dishes[i], quantity[i]);
            }

            if(address != null)
            {
                _addressOrderService.AddressOrder(address, order);
            }
        }


    }
}
