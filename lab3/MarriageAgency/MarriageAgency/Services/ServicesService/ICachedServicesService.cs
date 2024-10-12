using DataLayer.Models;

namespace MarriageAgency.Services.ServicesService
{
    public interface ICachedServicesService
    {
        public IEnumerable<Service> GetServices(int rowNumber);
        public void AddServices(string cacheKey, int rowNumber);
        public IEnumerable<Service> GetServices(string cacheKey, int rowNumber);
    }
}