namespace EFCoreLINQ.Models;

public partial class PhysicalAttribute
{
    public int ClientId { get; set; }

    public int? Age { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public int? ChildrenCount { get; set; }

    public string? MaritalStatus { get; set; }

    public string? BadHabits { get; set; }

    public string? Hobbies { get; set; }

    public virtual Client Client { get; set; } = null!;
}
