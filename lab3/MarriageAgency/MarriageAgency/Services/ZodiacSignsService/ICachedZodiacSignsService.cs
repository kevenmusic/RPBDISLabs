using DataLayer.Models;

namespace MarriageAgency.Services.ZodiacSignsService
{
    public interface ICachedZodiacSignsService
    {
        public IEnumerable<ZodiacSign> GetZodiacSigns(int rowNumber);
        public void AddZodiacSigns(string cacheKey, int rowNumber);
        public IEnumerable<ZodiacSign> GetZodiacSigns(string cacheKey, int rowNumber);
    }
}
