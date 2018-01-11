﻿// <auto-generated />
using BuildingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BuildingService.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BuildingService.Models.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeCount");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("BuildingService.Models.Employees", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DepartmentID");

                    b.Property<string>("First_name");

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("Last_Name");

                    b.Property<string>("Position");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BuildingService.Models.Employees", b =>
                {
                    b.HasOne("BuildingService.Models.Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID");
                });
#pragma warning restore 612, 618
        }
    }
}