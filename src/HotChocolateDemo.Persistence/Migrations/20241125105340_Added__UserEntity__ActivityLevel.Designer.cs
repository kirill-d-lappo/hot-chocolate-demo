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
    [Migration("20241125105340_Added__UserEntity__ActivityLevel")]
    partial class Added__UserEntity__ActivityLevel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.PermissionEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Key = "create:user"
                        },
                        new
                        {
                            Id = 2L,
                            Key = "update:user"
                        },
                        new
                        {
                            Id = 3L,
                            Key = "delete:user"
                        },
                        new
                        {
                            Id = 4L,
                            Key = "delete:user"
                        });
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.RoleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "driver"
                        });
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

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 1L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 2L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 3L
                        },
                        new
                        {
                            RoleId = 2L,
                            PermissionId = 3L
                        });
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<short>("ActivityLevel")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UserName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Users", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ActivityLevel = (short)0,
                            UserName = "klappo"
                        },
                        new
                        {
                            Id = 2L,
                            ActivityLevel = (short)0,
                            UserName = "dmutrov"
                        });
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

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            RoleId = 1L
                        },
                        new
                        {
                            UserId = 1L,
                            RoleId = 2L
                        },
                        new
                        {
                            UserId = 2L,
                            RoleId = 2L
                        });
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.RolePermissionEntity", b =>
                {
                    b.HasOne("HotChocolateDemo.Persistence.Models.PermissionEntity", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotChocolateDemo.Persistence.Models.RoleEntity", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.UserRoleEntity", b =>
                {
                    b.HasOne("HotChocolateDemo.Persistence.Models.RoleEntity", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotChocolateDemo.Persistence.Models.UserEntity", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.PermissionEntity", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.RoleEntity", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("HotChocolateDemo.Persistence.Models.UserEntity", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
