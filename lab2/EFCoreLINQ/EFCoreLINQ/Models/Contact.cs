namespace EFCoreLINQ.Models;

public partial class Contact
{
    public int ClientId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? PassportData { get; set; }

    public virtual Client Client { get; set; } = null!;
}
