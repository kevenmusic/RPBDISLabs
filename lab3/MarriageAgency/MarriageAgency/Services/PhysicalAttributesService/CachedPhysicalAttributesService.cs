using DataLayer.Data;
using DataLayer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MarriageAgency.Services.PhysicalAttributesService
{
    public class CachedPhysicalAttributesService : ICachedPhysicalAttributesService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedPhysicalAttributesService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddPhysicalAttributes(string cacheKey, int rowNumber)
        {
            IEnumerable<PhysicalAttribute> physicalAttributes = _context.PhysicalAttributes.Take(rowNumber).ToList();
            if (physicalAttributes != null)
            {
                _cache.Set(cacheKey, physicalAttributes, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<PhysicalAttribute> GetPhysicalAttributes(int rowNumber)
        {
            return _context.PhysicalAttributes.Take(rowNumber).ToList();
        }

        public IEnumerable<PhysicalAttribute> GetPhysicalAttributes(string cacheKey, int rowNumber)
        {
            IEnumerable<PhysicalAttribute> physicalAttributes;
            if (!_cache.TryGetValue(cacheKey, out physicalAttributes))
            {
                physicalAttributes = _context.PhysicalAttributes.Take(rowNumber).ToList();
                if (physicalAttributes != null)
                {
                    _cache.Set(cacheKey, physicalAttributes, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return physicalAttributes;
        }
    }
}