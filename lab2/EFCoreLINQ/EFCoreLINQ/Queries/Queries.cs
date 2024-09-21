using System;
using System.Linq;
using EFCoreLINQ.Data;
using EFCoreLINQ.Models;

namespace EFCoreLINQ.Queries
{
    public class Queries
    {
        private readonly MarriageAgencyContext _context;

        public Queries(MarriageAgencyContext context)
        {
            _context = context;
        }

        public IQueryable<Client> GetAllClients(int recordsNumber)
        {
            return _context.Clients.Take(recordsNumber);
        }

        public IQueryable<Client> FilterClientsByProfession(string profession, int recordsNumber)
        {
            return _context.Clients
                .Where(c => c.Profession == profession)
                .Take(recordsNumber);
        }

        public IQueryable<object> GroupClientsByProfession(int recordsNumber)
        {
            return _context.Clients
                .GroupBy(c => c.Profession)
                .Select(g => new
                {
                    Profession = g.Key,
                    ClientsCount = g.Count(),
                    MinHeight = g.Min(c => c.PhysicalAttribute != null ? c.PhysicalAttribute.Height : (decimal?)null), // Минимальный рост
                    MaxHeight = g.Max(c => c.PhysicalAttribute != null ? c.PhysicalAttribute.Height : (decimal?)null), // Максимальный рост
                    AvgHeight = g.Average(c => c.PhysicalAttribute != null ? c.PhysicalAttribute.Height : (decimal?)null), // Средний рост
                    Clients = g.Take(recordsNumber).ToList()
                });
        }


        public IQueryable<object> GetClientServices(int recordsNumber)
        {
            return (from service in _context.Services
                    join additionalService in _context.AdditionalServices
                    on service.AdditionalServiceId equals additionalService.AdditionalServiceId
                    select new
                    {
                        ClientName = service.Client.FirstName + " " + service.Client.LastName,
                        ServiceName = additionalService.Name,
                        service.Cost,
                        service.Date
                    }).Take(recordsNumber);
        }

        public IQueryable<object> FilterClientsByNationality(string nationality, int recordsNumber)
        {
            return (from client in _context.Clients
                    join nat in _context.Nationalities
                    on client.NationalityId equals nat.NationalityId
                    where nat.Name.Contains(nationality)
                    select new
                    {
                        ClientName = client.FirstName + " " + client.LastName,
                        Nationality = nat.Name,
                        client.Profession
                    }).Take(recordsNumber);
        }

        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void DeleteClient(string firstName, string lastName)
        {
            var client = _context.Clients
                .FirstOrDefault(c => c.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                                  && c.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }

        public void DeleteService(int serviceId)
        {
            var service = _context.Services.Find(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }

        public void UpdateClientRecords()
        {
            const int ageThreshold = 30;

            var clientsToUpdate = from client in _context.Clients
                                  join attr in _context.PhysicalAttributes
                                  on client.ClientId equals attr.ClientId
                                  where attr.Age > ageThreshold
                                  select client;

            foreach (var client in clientsToUpdate.ToList())
            {
                client.Profession = "Обновленная профессия";
            }

            _context.SaveChanges();
        }
    }
}