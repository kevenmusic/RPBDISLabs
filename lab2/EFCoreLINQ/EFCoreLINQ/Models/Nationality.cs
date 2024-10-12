namespace EFCoreLINQ.Models;

public partial class Nationality
{
    public int NationalityId { get; set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
