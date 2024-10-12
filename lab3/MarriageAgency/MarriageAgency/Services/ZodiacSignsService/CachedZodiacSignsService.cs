using DataLayer.Data;
using DataLayer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MarriageAgency.Services.ZodiacSignsService
{
    public class CachedZodiacSignsService : ICachedZodiacSignsService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedZodiacSignsService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddZodiacSigns(string cacheKey, int rowNumber)
        {
            IEnumerable<ZodiacSign> zodiacSigns = _context.ZodiacSigns.Take(rowNumber).ToList();
            if (zodiacSigns != null)
            {
                _cache.Set(cacheKey, zodiacSigns, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<ZodiacSign> GetZodiacSigns(int rowNumber)
        {
            return _context.ZodiacSigns.Take(rowNumber).ToList();
        }

        public IEnumerable<ZodiacSign> GetZodiacSigns(string cacheKey, int rowNumber)
        {
            IEnumerable<ZodiacSign> zodiacSigns;
            if (!_cache.TryGetValue(cacheKey, out zodiacSigns))
            {
                zodiacSigns = _context.ZodiacSigns.Take(rowNumber).ToList();
                if (zodiacSigns != null)
                {
                    _cache.Set(cacheKey, zodiacSigns, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return zodiacSigns;
        }
    }
}
