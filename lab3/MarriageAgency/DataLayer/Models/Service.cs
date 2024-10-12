namespace DataLayer.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public int AdditionalServiceId { get; set; }

    public int ClientId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly Date { get; set; }

    public decimal Cost { get; set; }

    public virtual AdditionalService AdditionalService { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
