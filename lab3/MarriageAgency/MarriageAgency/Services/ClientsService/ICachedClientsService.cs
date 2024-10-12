using DataLayer.Models;

namespace MarriageAgency.Services.ClientsService
{
    public interface ICachedClientsService
    {
        public IEnumerable<Client> GetClients(int rowNumber);
        public void AddClients(string cacheKey, int rowNumber);
        public IEnumerable<Client> GetClients(string cacheKey, int rowNumber);
    }
}
