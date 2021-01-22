using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreateDb.Storage;
using CreateDb.Storage.Models;
using CreateDb.TestDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CreateDb
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            ApplyMigrations();
            AddDataToTable testDB = new AddDataToTable();
            testDB.AddToBascet(_scopeFactory);
            
        }
        private void ApplyMigrations()
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
            _context.Database.Migrate();
        }

        
        //private void AllDelete()
        //{
        //    using var scope = _scopeFactory.CreateScope();
        //    var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();

        //var user = _context.Users.FirstOrDefault();
        //var menu = _context.Menu
        //    .Where(m => m.Id > 0)
        //    .Select(m => m);
        //foreach (var m in menu)
        //{
        //    _context.Menu.Remove(m);
        //}


        //    _context.Users.Remove(user);

        //    _context.SaveChanges();
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
