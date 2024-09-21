using System;
using System.Collections.Generic;

namespace EFCoreLINQ.Models;

public partial class EmployeeService
{
    public int EmployeeId { get; set; }

    public string EmployeeFirstName { get; set; } = null!;

    public string EmployeeLastName { get; set; } = null!;

    public string? Position { get; set; }

    public int ServiceId { get; set; }

    public int ClientId { get; set; }

    public string ClientFirstName { get; set; } = null!;

    public string ClientLastName { get; set; } = null!;

    public DateOnly ServiceDate { get; set; }

    public decimal Cost { get; set; }

    public string ServiceName { get; set; } = null!;
}
