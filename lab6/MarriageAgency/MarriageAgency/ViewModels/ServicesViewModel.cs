using MarriageAgency.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarriageAgency.ViewModels
{
    public class ServicesViewModel
    {
        [Display(Name = "Код услуги")]
        [Key]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Дополнительная услуга обязательна.")]
        [ForeignKey("AdditionalService")]
        [Display(Name = "Код дополнительной услуги")]
        public int AdditionalServiceId { get; set; }

        [Required(ErrorMessage = "Клиент обязателен.")]
        [ForeignKey("Client")]
        [Display(Name = "Код клиента")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Сотрудник обязателен.")]
        [ForeignKey("Employee")]
        [Display(Name = "Код сотрудника")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Дата обязательна.")]
        [Display(Name = "Дата услуги")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Стоимость обязательна.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Стоимость должна быть положительным числом.")]
        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }

        [Display(Name = "Дополнительная услуга")]
        public string AdditionalServiceName { get; set; } = null!;

        [Display(Name = "Клиент")]
        public string ClientName { get; set; } = null!;

        [Display(Name = "Сотрудник")]
        public string EmployeeName { get; set; } = null!;
    }
}
