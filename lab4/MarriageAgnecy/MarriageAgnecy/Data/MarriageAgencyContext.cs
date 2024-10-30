using Microsoft.EntityFrameworkCore;
using MarriageAgency.Models;

namespace MarriageAgency.Data;

public partial class MarriageAgencyContext(DbContextOptions<MarriageAgencyContext> options) : DbContext(options)
{
    public virtual DbSet<AdditionalService> AdditionalServices { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<PhysicalAttribute> PhysicalAttributes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ZodiacSign> ZodiacSigns { get; set; }
}
