using DataLayer.Data;
using DataLayer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MarriageAgency.Services.EmployeesService
{
    public class CachedEmployeesService : ICachedEmployeesService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedEmployeesService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddEmployees(string cacheKey, int rowNumber)
        {
            IEnumerable<Employee> employees = _context.Employees.Take(rowNumber).ToList();
            if (employees != null)
            {
                _cache.Set(cacheKey, employees, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<Employee> GetEmployees(int rowNumber)
        {
            return _context.Employees.Take(rowNumber).ToList();
        }

        public IEnumerable<Employee> GetEmployees(string cacheKey, int rowNumber)
        {
            IEnumerable<Employee> employees;
            if (!_cache.TryGetValue(cacheKey, out employees))
            {
                employees = _context.Employees.Take(rowNumber).ToList();
                if (employees != null)
                {
                    _cache.Set(cacheKey, employees, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return employees;
        }
    }
}
