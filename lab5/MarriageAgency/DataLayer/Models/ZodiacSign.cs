﻿using System.ComponentModel.DataAnnotations;

namespace MarriageAgency.DataLayer.Models
{
    public partial class ZodiacSign
    {
        [Display(Name = "Код знака зодиака")]
        public int ZodiacSignId { get; set; }

        [Display(Name = "Название знака")]
        public string Name { get; set; } = null!;

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Клиенты")]
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}