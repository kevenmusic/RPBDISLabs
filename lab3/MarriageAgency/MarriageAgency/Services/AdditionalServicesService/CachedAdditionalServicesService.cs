using Microsoft.Extensions.Caching.Memory;
using DataLayer.Data;
using DataLayer.Models;

namespace MarriageAgency.Services.AdditionalServicesService
{
    public class CachedAdditionalServicesService : ICachedAdditionalServicesService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedAdditionalServicesService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddAdditionalServices(string cacheKey, int rowNumber)
        {
            IEnumerable<AdditionalService> additionalServices = _context.AdditionalServices.Take(rowNumber).ToList();
            if (additionalServices != null)
            {
                _cache.Set(cacheKey, additionalServices, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<AdditionalService> GetAdditionalServices(int rowNumber)
        {
            return _context.AdditionalServices.Take(rowNumber).ToList();
        }

        public IEnumerable<AdditionalService> GetAdditionalServices(string cacheKey, int rowNumber)
        {
            IEnumerable<AdditionalService> additionalServices;
            if (!_cache.TryGetValue(cacheKey, out additionalServices))
            {
                additionalServices = _context.AdditionalServices.Take(rowNumber).ToList();
                if (additionalServices != null)
                {
                    _cache.Set(cacheKey, additionalServices, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return additionalServices;
        }
    }
}