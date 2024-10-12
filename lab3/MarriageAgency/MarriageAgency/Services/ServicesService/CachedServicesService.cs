using Microsoft.Extensions.Caching.Memory;
using DataLayer.Data;
using DataLayer.Models;

namespace MarriageAgency.Services.ServicesService
{
    public class CachedServicesService : ICachedServicesService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedServicesService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddServices(string cacheKey, int rowNumber)
        {
            IEnumerable<Service> services = _context.Services.Take(rowNumber).ToList();
            if (services != null)
            {
                _cache.Set(cacheKey, services, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<Service> GetServices(int rowNumber)
        {
            return _context.Services.Take(rowNumber).ToList();
        }

        public IEnumerable<Service> GetServices(string cacheKey, int rowNumber)
        {
            IEnumerable<Service> services;
            if (!_cache.TryGetValue(cacheKey, out services))
            {
                services = _context.Services.Take(rowNumber).ToList();
                if (services != null)
                {
                    _cache.Set(cacheKey, services, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return services;
        }
    }
}