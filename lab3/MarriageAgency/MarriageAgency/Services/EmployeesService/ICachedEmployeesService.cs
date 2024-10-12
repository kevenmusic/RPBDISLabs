using DataLayer.Models;

namespace MarriageAgency.Services.EmployeesService
{
    public interface ICachedEmployeesService
    {
        public IEnumerable<Employee> GetEmployees(int rowNumber);
        public void AddEmployees(string cacheKey, int rowNumber);
        public IEnumerable<Employee> GetEmployees(string cacheKey, int rowNumber);
    }
}
