namespace EFCoreLINQ.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Position { get; set; }

    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
