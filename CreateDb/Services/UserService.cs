using System;
using System.Linq;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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

        public void AllUsers()
        {

        }

        public void SelectUser()
        {

        }

        public void RegistrationUser() 
        {

        }

        public void DeleteUser()
        {

        }

        public void EditUser()
        {

        }
    }
}
