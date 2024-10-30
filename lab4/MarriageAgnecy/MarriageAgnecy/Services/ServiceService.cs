using MarriageAgency.ViewModels;
using MarriageAgency.Data;

namespace MarriageAgency.Services
{
    // Класс выборки 10 записей из всех таблиц 
    public class ServiceService(MarriageAgencyContext context) : IServiceService
    {
        private readonly MarriageAgencyContext _context = context;

        public HomeViewModel GetHomeViewModel(int numberRows = 10)
        {
            var clients = _context.Clients.Take(numberRows).ToList();
            var employees = _context.Employees.Take(numberRows).ToList();
            List<ServiceViewModel> services = [.. _context.Services
                .OrderByDescending(d => d.Date)
                .Select(s => new ServiceViewModel
                {
                    ServiceId = s.ServiceId,
                    Date = s.Date,
                    Cost = s.Cost
                })
                .Take(numberRows)];

            HomeViewModel homeViewModel = new()
            {
                Clients = clients,
                Employees = employees,
                Services = services
            };
            return homeViewModel;
        }
    }
}
