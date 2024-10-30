using MarriageAgency.Data;
using MarriageAgency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ClientsController : Controller
{
    private readonly MarriageAgencyContext _context;

    public ClientsController(MarriageAgencyContext context)
    {
        _context = context;
    }

    // GET: Clients
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 264)]
    public async Task<IActionResult> Index()
    {
        // Включение всех зависимостей
        var clients = await _context.Clients
            .Include(c => c.Contact)
            .Include(c => c.Nationality)
            .Include(c => c.PhysicalAttribute)
            .Include(c => c.ZodiacSign)
            .Include(c => c.Photo) // Если есть такая связь
            .ToListAsync();

        return View(clients);
    }
}
