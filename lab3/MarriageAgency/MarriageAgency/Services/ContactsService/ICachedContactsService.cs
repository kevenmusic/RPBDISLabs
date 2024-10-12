using DataLayer.Models;

namespace MarriageAgency.Services.ContactsService
{
    public interface ICachedContactsService
    {
        public IEnumerable<Contact> GetContacts(int rowNumber);
        public void AddContacts(string cacheKey, int rowNumber);
        public IEnumerable<Contact> GetContacts(string cacheKey, int rowNumber);
    }
}
