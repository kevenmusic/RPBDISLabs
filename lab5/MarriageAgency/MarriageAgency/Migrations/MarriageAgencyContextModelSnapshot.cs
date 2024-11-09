﻿// <auto-generated />
using System;
using MarriageAgency.DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MarriageAgency.Migrations
{
    [DbContext(typeof(MarriageAgencyContext))]
    partial class MarriageAgencyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.AdditionalService", b =>
                {
                    b.Property<int>("AdditionalServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdditionalServiceId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AdditionalServiceId");

                    b.ToTable("AdditionalServices");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationalityId")
                        .HasColumnType("int");

                    b.Property<string>("Profession")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZodiacSignId")
                        .HasColumnType("int");

                    b.HasKey("ClientId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("ZodiacSignId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Contact", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateOnly?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Nationality", b =>
                {
                    b.Property<int>("NationalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NationalityId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NationalityId");

                    b.ToTable("Nationalities");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Photo", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ClientPhoto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.PhysicalAttribute", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("BadHabits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ChildrenCount")
                        .HasColumnType("int");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Hobbies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ClientId");

                    b.ToTable("PhysicalAttributes");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<int>("AdditionalServiceId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("ServiceId");

                    b.HasIndex("AdditionalServiceId");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.ZodiacSign", b =>
                {
                    b.Property<int>("ZodiacSignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZodiacSignId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZodiacSignId");

                    b.ToTable("ZodiacSigns");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Client", b =>
                {
                    b.HasOne("MarriageAgency.DataLayer.Models.Nationality", "Nationality")
                        .WithMany("Clients")
                        .HasForeignKey("NationalityId");

                    b.HasOne("MarriageAgency.DataLayer.Models.ZodiacSign", "ZodiacSign")
                        .WithMany("Clients")
                        .HasForeignKey("ZodiacSignId");

                    b.Navigation("Nationality");

                    b.Navigation("ZodiacSign");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Contact", b =>
                {
                    b.HasOne("MarriageAgency.DataLayer.Models.Client", "Client")
                        .WithOne("Contact")
                        .HasForeignKey("MarriageAgency.DataLayer.Models.Contact", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Photo", b =>
                {
                    b.HasOne("MarriageAgency.DataLayer.Models.Client", "Client")
                        .WithOne("Photo")
                        .HasForeignKey("MarriageAgency.DataLayer.Models.Photo", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.PhysicalAttribute", b =>
                {
                    b.HasOne("MarriageAgency.DataLayer.Models.Client", "Client")
                        .WithOne("PhysicalAttribute")
                        .HasForeignKey("MarriageAgency.DataLayer.Models.PhysicalAttribute", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Service", b =>
                {
                    b.HasOne("MarriageAgency.DataLayer.Models.AdditionalService", "AdditionalService")
                        .WithMany("Services")
                        .HasForeignKey("AdditionalServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarriageAgency.DataLayer.Models.Client", "Client")
                        .WithMany("Services")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarriageAgency.DataLayer.Models.Employee", "Employee")
                        .WithMany("Services")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdditionalService");

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.AdditionalService", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Client", b =>
                {
                    b.Navigation("Contact");

                    b.Navigation("Photo");

                    b.Navigation("PhysicalAttribute");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Employee", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.Nationality", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("MarriageAgency.DataLayer.Models.ZodiacSign", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
