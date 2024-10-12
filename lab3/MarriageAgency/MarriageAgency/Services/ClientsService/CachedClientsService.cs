using Microsoft.Extensions.Caching.Memory;
using DataLayer.Data;
using DataLayer.Models;

namespace MarriageAgency.Services.ClientsService
{
    public class CachedClientsService : ICachedClientsService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedClientsService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddClients(string cacheKey, int rowNumber)
        {
            IEnumerable<Client> clients = _context.Clients.Take(rowNumber).ToList();
            if (clients != null)
            {
                _cache.Set(cacheKey, clients, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<Client> GetClients(int rowNumber)
        {
            return _context.Clients.Take(rowNumber).ToList();
        }

        public IEnumerable<Client> GetClients(string cacheKey, int rowNumber)
        {
            IEnumerable<Client> clients;
            if (!_cache.TryGetValue(cacheKey, out clients))
            {
                clients = _context.Clients.Take(rowNumber).ToList();
                if (clients != null)
                {
                    _cache.Set(cacheKey, clients, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return clients;
        }
    }
}
