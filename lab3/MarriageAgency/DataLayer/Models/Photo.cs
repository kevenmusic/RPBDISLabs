using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public partial class Photo
{
    [Key]
    public int ClientId { get; set; }

    public string ClientPhoto { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
