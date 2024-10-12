using DataLayer.Models;

namespace MarriageAgency.Services.AdditionalServicesService
{
    public interface ICachedAdditionalServicesService
    {
        public IEnumerable<AdditionalService> GetAdditionalServices(int rowNumber);
        public void AddAdditionalServices(string cacheKey, int rowNumber);
        public IEnumerable<AdditionalService> GetAdditionalServices(string cacheKey, int rowNumber);
    }
}