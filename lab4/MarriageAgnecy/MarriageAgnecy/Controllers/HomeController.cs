using Microsoft.AspNetCore.Mvc;
using MarriageAgency.Data;
using MarriageAgency.Models;
using MarriageAgency.ViewModels;
using System.Linq;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Threading.Tasks;

namespace MarriageAgency.Controllers
{
    public class HomeController(MarriageAgencyContext db) : Controller
    {
        private readonly MarriageAgencyContext _db = db;

        public IActionResult Index()
        {
            int numberRows = 10;
            List<Client> clients = [.. _db.Clients.Take(numberRows)];
            List<Employee> employees = [.. _db.Employees.Take(numberRows)];
            List<ServiceViewModel> services = [.. _db.Services
                .OrderByDescending(d => d.Date)
                .Select(s => new ServiceViewModel { 
                    ServiceId = s.ServiceId,
                    Employee = s.Employee,
                    Client = s.Client,
                    AdditionalService = s.AdditionalService,
                    Date = s.Date,
                    Cost = s.Cost,
                })
                .Take(numberRows)];

            HomeViewModel homeViewModel = new() { Clients = clients, Employees = employees, Services = services };
            return View(homeViewModel);
        }
    }
}