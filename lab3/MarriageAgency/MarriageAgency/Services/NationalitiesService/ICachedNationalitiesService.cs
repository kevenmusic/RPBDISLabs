using DataLayer.Models;

namespace MarriageAgency.Services.NationalitiesService
{
    public interface ICachedNationalitiesService
    {
        public IEnumerable<Nationality> GetNationalities(int rowNumber);
        public void AddNationalities(string cacheKey, int rowNumber);
        public IEnumerable<Nationality> GetNationalities(string cacheKey, int rowNumber);
    }
}
