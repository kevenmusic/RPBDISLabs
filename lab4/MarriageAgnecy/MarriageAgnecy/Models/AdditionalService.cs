﻿using System.ComponentModel.DataAnnotations;

namespace MarriageAgency.Models
{
    public partial class AdditionalService
    {
        [Display(Name = "Код доп. услуги")]
        public int AdditionalServiceId { get; set; }

        [Display(Name = "Название услуги")]
        public string Name { get; set; } = null!;

        [Display(Name = "Описание услуги")]
        public string? Description { get; set; }

        [Display(Name = "Цена услуги")]
        public decimal Price { get; set; }

        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
