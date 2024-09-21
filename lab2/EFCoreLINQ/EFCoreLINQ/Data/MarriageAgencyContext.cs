using System;
using System.Collections.Generic;
using EFCoreLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLINQ.Data;

public partial class MarriageAgencyContext : DbContext
{
    public MarriageAgencyContext()
    {
    }

    public MarriageAgencyContext(DbContextOptions<MarriageAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalService> AdditionalServices { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientFullInfo> ClientFullInfos { get; set; }

    public virtual DbSet<ClientService> ClientServices { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeService> EmployeeServices { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhysicalAttribute> PhysicalAttributes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ZodiacSign> ZodiacSigns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=HOME-PC;Database=MarriageAgency; Trusted_Connection =True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalService>(entity =>
        {
            entity.HasKey(e => e.AdditionalServiceId).HasName("PK__Addition__F92FC43E93B718A2");

            entity.HasIndex(e => e.Name, "UQ__Addition__737584F6E2706B41").IsUnique();

            entity.Property(e => e.AdditionalServiceId).HasColumnName("AdditionalServiceID");
            entity.Property(e => e.Description).HasDefaultValue("Нет описания");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A04EBD6C7DE");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.NationalityId).HasColumnName("NationalityID");
            entity.Property(e => e.Profession)
                .HasMaxLength(100)
                .HasDefaultValue("Безработный");
            entity.Property(e => e.ZodiacSignId).HasColumnName("ZodiacSignID");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Clients)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Clients__Nationa__4222D4EF");

            entity.HasOne(d => d.ZodiacSign).WithMany(p => p.Clients)
                .HasForeignKey(d => d.ZodiacSignId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Clients__ZodiacS__412EB0B6");
        });

        modelBuilder.Entity<ClientFullInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ClientFullInfo");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MaritalStatus).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PassportData).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Profession).HasMaxLength(100);
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ZodiacSign).HasMaxLength(50);
        });

        modelBuilder.Entity<ClientService>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ClientServices");

            entity.Property(e => e.ClientFirstName).HasMaxLength(50);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientLastName).HasMaxLength(50);
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeFirstName).HasMaxLength(50);
            entity.Property(e => e.EmployeeLastName).HasMaxLength(50);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Contacts__E67E1A0495336D48");

            entity.HasIndex(e => e.Phone, "UQ__Contacts__5C7E359E823E174F").IsUnique();

            entity.HasIndex(e => e.PassportData, "UQ__Contacts__E8994A81C0646797").IsUnique();

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.PassportData).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.Client).WithOne(p => p.Contact)
                .HasForeignKey<Contact>(d => d.ClientId)
                .HasConstraintName("FK__Contacts__Client__47DBAE45");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1A6174395");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasDefaultValue("Неизвестный");
        });

        modelBuilder.Entity<EmployeeService>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployeeServices");

            entity.Property(e => e.ClientFirstName).HasMaxLength(50);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientLastName).HasMaxLength(50);
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeFirstName).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeLastName).HasMaxLength(50);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(100);
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.NationalityId).HasName("PK__National__F628E7A472623B36");

            entity.HasIndex(e => e.Name, "UQ__National__737584F6FE3F4DE8").IsUnique();

            entity.Property(e => e.NationalityId).HasColumnName("NationalityID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Notes).HasDefaultValue("Нет доступных заметок");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Photos__E67E1A043F13E97A");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.Photo1).HasColumnName("Photo");

            entity.HasOne(d => d.Client).WithOne(p => p.Photo)
                .HasForeignKey<Photo>(d => d.ClientId)
                .HasConstraintName("FK__Photos__ClientID__52593CB8");
        });

        modelBuilder.Entity<PhysicalAttribute>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Physical__E67E1A04E7B1AE3E");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Одинокий");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Client).WithOne(p => p.PhysicalAttribute)
                .HasForeignKey<PhysicalAttribute>(d => d.ClientId)
                .HasConstraintName("FK__PhysicalA__Clien__4AB81AF0");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA6F6C8CDE");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.AdditionalServiceId).HasColumnName("AdditionalServiceID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.AdditionalService).WithMany(p => p.Services)
                .HasForeignKey(d => d.AdditionalServiceId)
                .HasConstraintName("FK__Services__Additi__5DCAEF64");

            entity.HasOne(d => d.Client).WithMany(p => p.Services)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Services__Client__5EBF139D");

            entity.HasOne(d => d.Employee).WithMany(p => p.Services)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Services__Employ__5FB337D6");
        });

        modelBuilder.Entity<ZodiacSign>(entity =>
        {
            entity.HasKey(e => e.ZodiacSignId).HasName("PK__ZodiacSi__48424122AA73AB1B");

            entity.HasIndex(e => e.Name, "UQ__ZodiacSi__737584F6CCE6814D").IsUnique();

            entity.Property(e => e.ZodiacSignId).HasColumnName("ZodiacSignID");
            entity.Property(e => e.Description).HasDefaultValue("Нет описания");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
