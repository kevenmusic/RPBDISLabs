using DataLayer.Data;
using DataLayer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MarriageAgency.Services.ContactsService
{
    public class CachedContactsService : ICachedContactsService
    {
        private readonly IMemoryCache _cache;
        private readonly MarriageAgencyContext _context;

        public CachedContactsService(IMemoryCache cache, MarriageAgencyContext context)
        {
            _cache = cache;
            _context = context;
        }

        public void AddContacts(string cacheKey, int rowNumber)
        {
            IEnumerable<Contact> contacts = _context.Contacts.Take(rowNumber).ToList();
            if (contacts != null)
            {
                _cache.Set(cacheKey, contacts, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(264)
                });
            }
        }

        public IEnumerable<Contact> GetContacts(int rowNumber)
        {
            return _context.Contacts.Take(rowNumber).ToList();
        }

        public IEnumerable<Contact> GetContacts(string cacheKey, int rowNumber)
        {
            IEnumerable<Contact> contacts;
            if (!_cache.TryGetValue(cacheKey, out contacts))
            {
                contacts = _context.Contacts.Take(rowNumber).ToList();
                if (contacts != null)
                {
                    _cache.Set(cacheKey, contacts, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(264)));
                }
            }
            return contacts;
        }
    }
}
