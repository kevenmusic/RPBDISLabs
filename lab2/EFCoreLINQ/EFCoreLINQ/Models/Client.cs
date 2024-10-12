namespace EFCoreLINQ.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public int? ZodiacSignId { get; set; }

    public int? NationalityId { get; set; }

    public string? Profession { get; set; }

    public virtual Contact? Contact { get; set; }

    public virtual Nationality? Nationality { get; set; }

    public virtual Photo? Photo { get; set; }

    public virtual PhysicalAttribute? PhysicalAttribute { get; set; }

    public virtual ICollection<Service> Services { get; set; } = [];

    public virtual ZodiacSign? ZodiacSign { get; set; }
}
