using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CreateDb.Services;
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
            //testDB.CreateMenu(_scopeFactory);
            //testDB.AddData(_scopeFactory);
            //testDB.AddToBascet(_scopeFactory);
            //testDB.AddAddress(_scopeFactory);
            MenuService fullMenu = new MenuService(scopeFactory);
            fullMenu.GetMenu();

        }
        private void ApplyMigrations()
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
            _context.Database.Migrate();
        }
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
