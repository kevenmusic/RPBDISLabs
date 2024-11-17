﻿using System.ComponentModel.DataAnnotations;

namespace MarriageAgency.Models
{
    public partial class Nationality
    {

        [Display(Name = "Код национальности")]
        public int NationalityId { get; set; }

        [Display(Name = "Название национальности")]
        [Required(ErrorMessage = "Поле Фамилия клиента обязательно для заполнения.")]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Числа не допускаются.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Примечания")]
        public string? Notes { get; set; }

        [Display(Name = "Клиенты")]
        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}