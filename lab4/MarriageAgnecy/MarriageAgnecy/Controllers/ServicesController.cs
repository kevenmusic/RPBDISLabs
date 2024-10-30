using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarriageAgency.Data;
using MarriageAgency.ViewModels;
using MarriageAgency.Models;

namespace MarriageAgency.Controllers
{
    public class ServicesController : Controller
    {
        private readonly MarriageAgencyContext _context;

        public ServicesController(MarriageAgencyContext context)
        {
            _context = context;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 264)]
        public async Task<IActionResult> Index(
            string clientsSearch,
            string employeesSearch,
            DateTime? startDate,
            DateTime? endDate,
            decimal? minCost,
            decimal? maxCost,
            int? page,
            SortState sortOrder
         )

        {
            // Получаем все услуги с клиентами и сотрудниками
            var servicesQuery = _context.Services
                .Include(s => s.Client)
                .Include(s => s.Employee)
                 .Include(s => s.AdditionalService)
                .AsQueryable();

            // Преобразуем в ServiceViewModel
            var serviceViewModels = await servicesQuery.Select(s => new ServiceViewModel
            {
                Date = s.Date,
                Cost = s.Cost,
                Client = s.Client,
                Employee = s.Employee,
                AdditionalService = s.AdditionalService
            }).ToListAsync();

            // Применяем фильтры
            var filteredServices = FilterServices(serviceViewModels.AsQueryable(), clientsSearch, employeesSearch, minCost, maxCost, startDate, endDate);

            // Применяем сортировку
            var sortedServices = SortServices(filteredServices, sortOrder);

            // Пагинация
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int totalCount = sortedServices.Count();
            var items = sortedServices
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Формируем ViewModel
            var viewModel = new ServicesViewModel
            {
                Services = items,
                PageViewModel = new PageViewModel(totalCount, pageNumber, pageSize),
                ServiceViewModel = new ServiceViewModel
                {
                    SortViewModel = new SortViewModel(sortOrder),
                }
            };

            return View(viewModel);
        }

        // Фильтрация для ServiceViewModel
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 264)]
        private IQueryable<ServiceViewModel> FilterServices(
            IQueryable<ServiceViewModel> services,
            string clientsSearch,
            string employeesSearch, // Новый параметр для фильтрации по имени сотрудника
            decimal? minCost,
            decimal? maxCost,
            DateTime? startDate,
            DateTime? endDate)
        {
            // Фильтрация по имени клиента
            if (!string.IsNullOrEmpty(clientsSearch))
            {
                services = services.Where(s => s.Client.FirstName.ToLower().Contains(clientsSearch.ToLower()));
            }

            // Фильтрация по имени сотрудника
            if (!string.IsNullOrEmpty(employeesSearch))
            {
                services = services.Where(s => s.Employee.FirstName.ToLower().Contains(employeesSearch.ToLower()));
            }

            // Фильтрация по минимальной стоимости
            if (minCost.HasValue)
            {
                services = services.Where(s => s.Cost >= minCost.Value);
            }

            // Фильтрация по максимальной стоимости
            if (maxCost.HasValue)
            {
                services = services.Where(s => s.Cost <= maxCost.Value);
            }

            // Фильтрация по дате начала
            if (startDate.HasValue)
            {
                DateOnly start = DateOnly.FromDateTime(startDate.Value);
                services = services.Where(s => s.Date >= start);
            }

            // Фильтрация по дате окончания
            if (endDate.HasValue)
            {
                DateOnly end = DateOnly.FromDateTime(endDate.Value);
                services = services.Where(s => s.Date <= end);
            }

            return services;
        }

        // Сортировка для ServiceViewModel
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 264)]
        private IQueryable<ServiceViewModel> SortServices(IQueryable<ServiceViewModel> services, SortState sortOrder)
        {
            return sortOrder switch
            {
                SortState.CostAsc => services.OrderBy(s => s.Cost),
                SortState.CostDesc => services.OrderByDescending(s => s.Cost),
                SortState.ClientNameAsc => services.OrderBy(s => s.Client.FirstName),
                SortState.ClientNameDesc => services.OrderByDescending(s => s.Client.FirstName),
                SortState.EmployeeNameAsc => services.OrderBy(s => s.Employee.FirstName),
                SortState.EmployeeNameDesc => services.OrderByDescending(s => s.Employee.FirstName),
                _ => services.OrderBy(s => s.Date),
            };
        }
    }
}
