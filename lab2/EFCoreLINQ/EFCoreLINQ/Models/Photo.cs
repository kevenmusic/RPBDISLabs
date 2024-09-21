using System;
using System.Collections.Generic;

namespace EFCoreLINQ.Models;

public partial class Photo
{
    public int ClientId { get; set; }

    public string Photo1 { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
