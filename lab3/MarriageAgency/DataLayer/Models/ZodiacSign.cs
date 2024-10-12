namespace DataLayer.Models;

public partial class ZodiacSign
{
    public int ZodiacSignId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
