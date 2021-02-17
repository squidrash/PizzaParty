using System;
using System.Collections.Generic;
using System.Linq;
using CreateDb.Services;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CreateDb.TestDB
{
    public interface ITestService
    {
        public void TestAddress();
    }
    public class TestService :ITestService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        
        private readonly IMenuService _menuService;
        
        private readonly IOrdersService _ordersService;
        
        private readonly IUserService _userService;
        
        private readonly IAddressesService _addressesService;

        public TestService(IServiceScopeFactory scopeFactory, IMenuService menuService, IOrdersService ordersService,
            IUserService userService, IAddressesService addressesService)
        {
            _scopeFactory = scopeFactory;
            _menuService = menuService;
            _ordersService = ordersService;
            _userService = userService;
            _addressesService = addressesService;
        }
        private void InitialData()
        {
            AddDataToTable testDB = new AddDataToTable();
            testDB.CreateMenu(_scopeFactory);
            testDB.AddCustomerAndOrders(_scopeFactory);
            testDB.AddToBascet(_scopeFactory);
            testDB.AddAddress(_scopeFactory);
        }
        private void TestAllOrders()// работает
        {
            Console.WriteLine("метод с передачей клиента");

            var user = _userService.SelectUserFromDb("Tom", "Smit", 1);
            Console.WriteLine($"{user.Name} {user.LastName}");
            Console.WriteLine($"Номер: {user.Phone} скидка: {user.Discount}");
            var orders = _ordersService.AllOrders(user);

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.CreatTime} - {order.CustomerEntityId} - status {order.Status} ");
            }

            Console.WriteLine("метод без клиента");

            var ord = _ordersService.AllOrders();
            foreach (var o in ord)
            {
                Console.WriteLine($"{o.CreatTime} - {o.CustomerEntityId} - status {o.Status} - {o.Customer.Name} {o.Customer.LastName}");
            }
        }
        private void TestCreateOrder()// ошибка в SaveChanges
        {
            var user = _userService.SelectUserFromDb("Tom", "Smit", 1);
            Console.WriteLine($"{user.Name} {user.LastName}");
            Console.WriteLine($"{user.Phone} {user.Discount}");

            //var order = _ordersCustomerService.CreateOrder(user);
            //Console.WriteLine("метод с передачей клиента");
            //Console.WriteLine($"{order.CreatTime} - {order.CustomerEntityId} - status {order.Status}");
            //var order1 = _ordersCustomerService.CreateOrder();
            //Console.WriteLine("метод без передачи клиента");
            //Console.WriteLine($"{order1.CreatTime} - {order1.CustomerEntityId} - status {order1.Status}");
            _ordersService.CreateOrder(user);

            _ordersService.CreateOrder();

        }
        private void TestChangeOrderStatus()// работает
        {
            var user = _userService.SelectUserFromDb("Tom", "Smit", 1);
            Console.WriteLine($"{user.Name} {user.LastName}");
            Console.WriteLine($"Номер: {user.Phone} скидка: {user.Discount}");
            var orders = user.Orders;
            foreach (var o in orders)
            {
                Console.WriteLine($"Время создания заказа: {o.CreatTime.TimeOfDay}б  статус заказа: {o.Status}");
                var products = o.Products;
                foreach (var p in products)
                {
                    Console.WriteLine($"Наименование блюда: {p.Dish.ProductName}, количество - {p.CountDish}");
                }
            }

            var order = user.Orders
                .Where(o => o.Status == Status.Preparing)
                .FirstOrDefault();
            var orderStatus = "Новый";
            Console.WriteLine($"статус до изменения {order.Status}");
            var result = _ordersService.ChangeOrderStatus(order, orderStatus);
            Console.WriteLine($"статус после изменения {result.Status}");
        }

        public void TestAddress()
        {
            Console.WriteLine("Метод без квартиры и подъезда");

            var city = "Буденновск";
            var street = "Пушкинская";
            var numbOfBuild = "77777";
            AddressEntity address1 = new AddressEntity
            {
                City = city,
                Street = street,
                NumberOfBuild = numbOfBuild
            };
            
            //_addressesService.CreateDeliveryAddress(city, street, numbOfBuild);

            Console.WriteLine("полный метод");
            var city2 = "Ставрополь";
            var street2 = "Ленина";
            var numbOfBuild2 = "10000";
            var numbOfEntrance2 = 1;
            var apartment = 27;
            AddressEntity address2 = new AddressEntity
            {
                City = city2,
                Street = street2,
                NumberOfBuild = numbOfBuild2,
                NumberOfEntrance = numbOfEntrance2,
                Apartment = apartment

            };

            var address3 = new AddressEntity
            {
                City = " абра ",
                Street = "кадабра",
                NumberOfBuild = "123456"
            };
            //_addressesService.CreateDeliveryAddress(city2, street2, nubmOfBuild2, numbOfEntrance2, apartment);
            

            var addres = _addressesService.GetDeliveryAddress(address1);
            var addres2 = _addressesService.GetDeliveryAddress(address2);
            var addres3 = _addressesService.GetDeliveryAddress(address3);
            List<AddressEntity> addresses = new List<AddressEntity>();

            addresses.Add(addres);
            addresses.Add(addres2);
            addresses.Add(addres3);

            //if (addres != null)
            //{
            //    addresses.Add(addres);
            //}
            //else
            //{
            //    Console.WriteLine("Объект не найден 1");
            //}
            //if (addres2 != null)
            //{
            //    addresses.Add(addres2);
            //}
            //else
            //{
            //    Console.WriteLine("Объект не найден 2");
            //}


            var i = 1;
            foreach (var a in addresses)
            {
                if (a != null)
                {
                    Console.WriteLine($"{a.Id} - {a.City}  {a.Street} {a.NumberOfBuild} {a.NumberOfEntrance} {a.Apartment}");

                    Console.WriteLine("--------------------------");
                }
                else
                {
                    Console.WriteLine($"Объект {i} не найден");
                }
                 ++i;
            }

        }
    }
}
