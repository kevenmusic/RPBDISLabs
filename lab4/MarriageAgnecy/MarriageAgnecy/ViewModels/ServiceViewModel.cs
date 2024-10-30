using System.ComponentModel.DataAnnotations;
using MarriageAgency.Models;

namespace MarriageAgency.ViewModels
{
    public class ServiceViewModel
    {
        [Display(Name = "Код услуги")]
        public int ServiceId { get; set; }

        [Display(Name = "Дата услуги")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Дата услуги обязательна.")]
        public DateOnly Date { get; set; }

        [Display(Name = "Стоимость")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Стоимость должна быть положительным числом.")]
        [Required(ErrorMessage = "Стоимость обязательна.")]
        public decimal Cost { get; set; }

        [Display(Name = "Дополнительная услуга")]
        public virtual AdditionalService AdditionalService { get; set; } = null!;

        [Display(Name = "Клиент")]
        public virtual Client Client { get; set; } = null!;

        [Display(Name = "Сотрудник")]
        public virtual Employee Employee { get; set; } = null!;

        public SortViewModel SortViewModel { get; set; }
    }
}
