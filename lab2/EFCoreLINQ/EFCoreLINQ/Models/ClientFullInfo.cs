using System;
using System.Collections.Generic;

namespace EFCoreLINQ.Models;

public partial class ClientFullInfo
{
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string? ZodiacSign { get; set; }

    public string? Nationality { get; set; }

    public string? Profession { get; set; }

    public int? Age { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public int? ChildrenCount { get; set; }

    public string? MaritalStatus { get; set; }

    public string? BadHabits { get; set; }

    public string? Hobbies { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? PassportData { get; set; }

    public string? Photo { get; set; }
}
