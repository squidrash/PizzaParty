﻿// <auto-generated />
using System;
using CreateDb.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CreateDb.Migrations
{
    [DbContext(typeof(PizzaDbContext))]
    partial class PizzaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CreateDb.Storage.Models.AddressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Apartment")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("CustomerEntityId")
                        .HasColumnType("integer");

                    b.Property<string>("NumberOfBuild")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumberOfEntrance")
                        .HasColumnType("integer");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerEntityId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.AddressOrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AddressEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressEntityId")
                        .IsUnique();

                    b.HasIndex("OrderEntityId")
                        .IsUnique();

                    b.ToTable("AddressOrderEntities");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Discount")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.MenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("CustomerEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerEntityId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.OrderMenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CountDish")
                        .HasColumnType("integer");

                    b.Property<int>("MenuEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MenuEntityId");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderMenuEntities");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.AddressEntity", b =>
                {
                    b.HasOne("CreateDb.Storage.Models.CustomerEntity", "CustomerAddress")
                        .WithMany("Address")
                        .HasForeignKey("CustomerEntityId");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.AddressOrderEntity", b =>
                {
                    b.HasOne("CreateDb.Storage.Models.AddressEntity", "Address")
                        .WithOne("AddressOrder")
                        .HasForeignKey("CreateDb.Storage.Models.AddressOrderEntity", "AddressEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CreateDb.Storage.Models.OrderEntity", "Order")
                        .WithOne("AddressOrder")
                        .HasForeignKey("CreateDb.Storage.Models.AddressOrderEntity", "OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CreateDb.Storage.Models.OrderEntity", b =>
                {
                    b.HasOne("CreateDb.Storage.Models.CustomerEntity", "CustomerOrder")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerEntityId");
                });

            modelBuilder.Entity("CreateDb.Storage.Models.OrderMenuEntity", b =>
                {
                    b.HasOne("CreateDb.Storage.Models.MenuEntity", "Dish")
                        .WithMany("Orders")
                        .HasForeignKey("MenuEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CreateDb.Storage.Models.OrderEntity", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
