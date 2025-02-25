﻿// <auto-generated />
using System;
using FlowerShopManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HW_6._1.Migrations
{
    [DbContext(typeof(FlowerShopContext))]
    [Migration("20240908124012_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlowerShopManagement.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("FlowerShopManagement.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("ShopId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FlowerShopManagement.Shop", b =>
                {
                    b.Property<int>("ShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShopId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ShopId");

                    b.HasIndex("CityId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("FlowerShopManagement.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("FlowerShopManagement.Product", b =>
                {
                    b.HasOne("FlowerShopManagement.Shop", "Shop")
                        .WithMany("Products")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("FlowerShopManagement.Shop", b =>
                {
                    b.HasOne("FlowerShopManagement.City", "City")
                        .WithMany("Shops")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowerShopManagement.Supplier", null)
                        .WithMany("Shops")
                        .HasForeignKey("SupplierId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("FlowerShopManagement.City", b =>
                {
                    b.Navigation("Shops");
                });

            modelBuilder.Entity("FlowerShopManagement.Shop", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FlowerShopManagement.Supplier", b =>
                {
                    b.Navigation("Shops");
                });
#pragma warning restore 612, 618
        }
    }
}
