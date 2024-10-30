using MarriageAgency.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarriageAgency.Controllers
{
    public class EmployeesController(MarriageAgencyContext context) : Controller
    {
        private readonly MarriageAgencyContext _context = context;

        // GET: Employees
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 264)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
    }
}