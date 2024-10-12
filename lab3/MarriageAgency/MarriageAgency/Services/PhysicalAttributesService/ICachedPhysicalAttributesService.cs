using DataLayer.Models;

namespace MarriageAgency.Services.PhysicalAttributesService
{
    public interface ICachedPhysicalAttributesService
    {
        public IEnumerable<PhysicalAttribute> GetPhysicalAttributes(int rowNumber);
        public void AddPhysicalAttributes(string cacheKey, int rowNumber);
        public IEnumerable<PhysicalAttribute> GetPhysicalAttributes(string cacheKey, int rowNumber);
    }
}