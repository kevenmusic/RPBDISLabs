using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MarriageAgency.Models;

namespace MarriageAgency.ViewModels
{
    public class ServicesViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }

        //Свойство для фильтрации
        public ServiceViewModel ServiceViewModel { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }
    }
}   
