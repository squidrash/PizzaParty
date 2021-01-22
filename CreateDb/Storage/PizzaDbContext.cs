using System;
using CreateDb.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CreateDb.Storage
{
    public class PizzaDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }
        public DbSet<BascetEntity> Bascet { get; set; }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base (options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
