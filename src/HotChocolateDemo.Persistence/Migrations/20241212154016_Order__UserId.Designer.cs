﻿// <auto-generated />
using System;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotChocolateDemo.Persistence.Migrations
{
    [DbContext(typeof(HCDemoDbContext))]
    [Migration("20241212154016_Order__UserId")]
    partial class Order__UserId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.Food", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Foods", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.FoodOrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<long>("FoodId")
                        .HasColumnType("bigint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("FoodOrderItems", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("OrderNumber")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.UserManagement.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique()
                        .HasFilter("[Key] IS NOT NULL");

                    b.ToTable("Permissions", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.UserManagement.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.UserManagement.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<short>("ActivityLevel")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("BirthDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ImageFileName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.RolePermissionEntity", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.UserRoleEntity", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "dbo");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.FoodOrderItem", b =>
                {
                    b.HasOne("HotChocolateDemo.Models.Orders.Food", "Food")
                        .WithMany("FoodOrderItems")
                        .HasForeignKey("FoodId")
                        .IsRequired();

                    b.HasOne("HotChocolateDemo.Models.Orders.Order", "Order")
                        .WithMany("FoodOrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.Order", b =>
                {
                    b.HasOne("HotChocolateDemo.Models.UserManagement.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.RolePermissionEntity", b =>
                {
                    b.HasOne("HotChocolateDemo.Models.UserManagement.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotChocolateDemo.Models.UserManagement.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.UserRoleEntity", b =>
                {
                    b.HasOne("HotChocolateDemo.Models.UserManagement.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotChocolateDemo.Models.UserManagement.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.Food", b =>
                {
                    b.Navigation("FoodOrderItems");
                });

            modelBuilder.Entity("HotChocolateDemo.Models.Orders.Order", b =>
                {
                    b.Navigation("FoodOrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
