using System;
using System.Collections.Generic;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.Services.CustomerActions
{
    public interface ICustomerActionsService
    {
        public List<MenuEntity> ReedFullMenu();
    }
    public class CustomerActionsService : ICustomerActionsService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMenuService _menuService;

        //вместо _customer должен быть залогиненный пользователь
        private CustomerEntity _customer;

        public CustomerActionsService(IServiceScopeFactory scopeFactory, IMenuService menuService, CustomerEntity customer = null)
        {
            _scopeFactory = scopeFactory;
            _menuService = menuService;
            _customer = customer;
        }
        public List<MenuEntity> ReedFullMenu()
        {
            var fullMenu = _menuService.GetMenu();
            foreach(var m in fullMenu)
            {
                Console.WriteLine($"{m.ProductName} - {m.Price}");
            }
            return fullMenu;
        }
        public void CreateOrder(CustomerEntity customer = null, AddressEntity address = null )
        {

        }


    }
}
