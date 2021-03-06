﻿using System;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateDb.Storage
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<MenuEntity> Menus { get; set; }
        public DbSet<OrderMenuEntity> OrderMenuEntities { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<AddressOrderEntity> AddressOrderEntities { get; set; }
        public DbSet<CustomerAddressEntity> CustomerAddressEntities { get; set; }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
