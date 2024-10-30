using System.Threading.Tasks;
using MarriageAgency.Models;
using Microsoft.Identity.Client.Extensions.Msal;

namespace MarriageAgency.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<ServiceViewModel> Services { get; set; }
    }
}
