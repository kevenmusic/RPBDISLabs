using DataLayer.Data;
using DataLayer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MarriageAgency.Services.NationalitiesService
{
    public class CachedNationalitiesService : ICachedNationalitiesService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedNationalitiesService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddNationalities(string cacheKey, int rowNumber)
        {
            IEnumerable<Nationality> nationalities = _context.Nationalities.Take(rowNumber).ToList();
            if (nationalities != null)
            {
                _cache.Set(cacheKey, nationalities, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<Nationality> GetNationalities(int rowNumber)
        {
            return _context.Nationalities.Take(rowNumber).ToList();
        }

        public IEnumerable<Nationality> GetNationalities(string cacheKey, int rowNumber)
        {
            IEnumerable<Nationality> nationalities;
            if (!_cache.TryGetValue(cacheKey, out nationalities))
            {
                nationalities = _context.Nationalities.Take(rowNumber).ToList();
                if (nationalities != null)
                {
                    _cache.Set(cacheKey, nationalities, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return nationalities;
        }
    }
}
