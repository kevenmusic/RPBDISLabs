using EFCoreLINQ.Data;
using EFCoreLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLINQ.Queries
{
    /// <summary>
    /// Класс для выполнения запросов к базе данных агентства по браку.
    /// </summary>
    public class Queries
    {
        private readonly MarriageAgencyContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Queries"/> с указанным контекстом базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public Queries(MarriageAgencyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получает всех клиентов с ограничением на количество записей.
        /// </summary>
        /// <param name="recordsNumber">Максимальное количество записей для выборки.</param>
        /// <returns>Запрос, содержащий всех клиентов.</returns>
        public IQueryable<Client> GetAllClients(int recordsNumber)
        {
            return _context.Clients.Take(recordsNumber);
        }

        /// <summary>
        /// Фильтрует клиентов по профессии с ограничением на количество записей.
        /// </summary>
        /// <param name="profession">Профессия для фильтрации.</param>
        /// <param name="recordsNumber">Максимальное количество записей для выборки.</param>
        /// <returns>Запрос, содержащий отфильтрованных клиентов.</returns>
        public IQueryable<Client> FilterClientsByProfession(string profession, int recordsNumber)
        {
            return _context.Clients
                .Where(c => c.Profession == profession)
                .Take(recordsNumber);
        }

        /// <summary>
        /// Группирует клиентов по профессии с расчетом статистики по росту.
        /// </summary>
        /// <param name="recordsNumber">Максимальное количество клиентов для выборки.</param>
        /// <returns>Запрос, содержащий группы клиентов и статистику по росту.</returns>
        public IQueryable<object> GroupClientsByProfession(int recordsNumber)
        {
            return _context.Clients
                .GroupBy(c => c.Profession)
                .Select(g => new
                {
                    Profession = g.Key,
                    ClientsCount = g.Count(),
                    MinHeight = g.Min(c => c.PhysicalAttribute.Height),
                    MaxHeight = g.Max(c => c.PhysicalAttribute.Height),
                    AvgHeight = g.Average(c => c.PhysicalAttribute.Height),
                    Clients = g.Take(recordsNumber).ToList() // Выбираем только recordsNumber клиентов
                });
        }

        /// <summary>
        /// Получает услуги клиентов с ограничением на количество записей.
        /// </summary>
        /// <param name="recordsNumber">Максимальное количество записей для выборки.</param>
        /// <returns>Запрос, содержащий услуги клиентов.</returns>
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

        /// <summary>
        /// Фильтрует клиентов по национальности с ограничением на количество записей.
        /// </summary>
        /// <param name="nationality">Название национальности для фильтрации.</param>
        /// <param name="recordsNumber">Максимальное количество записей для выборки.</param>
        /// <returns>Запрос, содержащий отфильтрованных клиентов.</returns>
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

        /// <summary>
        /// Добавляет нового клиента в базу данных.
        /// </summary>
        /// <param name="client">Клиент для добавления.</param>
        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        /// <summary>
        /// Добавляет новую услугу в базу данных.
        /// </summary>
        /// <param name="service">Услуга для добавления.</param>
        public void AddService(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет клиента по имени и фамилии.
        /// </summary>
        /// <param name="firstName">Имя клиента.</param>
        /// <param name="lastName">Фамилия клиента.</param>
        /// <returns>Удаленный клиент или null, если клиент не найден.</returns>
        public Client DeleteClient(string firstName, string lastName)
        {
            var client = _context.Clients
                .FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName); // Простой поиск по имени и фамилии

            if (client != null)
            {
                _context.Clients.Remove(client); // Если клиент найден, удаляем
                _context.SaveChanges(); // Сохраняем изменения
            }

            return client; // Возвращаем удаленного клиента или null, если не найден
        }

        /// <summary>
        /// Удаляет услугу по идентификатору.
        /// </summary>
        /// <param name="serviceId">Идентификатор услуги.</param>
        /// <returns>Удаленная услуга или null, если услуга не найдена.</returns>
        public Service DeleteService(int serviceId)
        {
            var service = _context.Services
                .Include(s => s.AdditionalService) // Загружаем дополнительную услугу
                .FirstOrDefault(s => s.ServiceId == serviceId); // Находим услугу по ID

            if (service != null)
            {
                _context.Services.Remove(service); // Если услуга найдена, удаляем
                _context.SaveChanges(); // Сохраняем изменения
            }

            return service; // Возвращаем удаленную услугу или null, если не найдена
        }

        /// <summary>
        /// Обновляет записи клиентов, удовлетворяющих заданному порогу возраста.
        /// </summary>
        /// <param name="ageThreshold">Порог возраста для обновления.</param>
        /// <returns>Запрос с обновленными записями клиентов.</returns>
        public IQueryable<Client> UpdateClientRecords(int ageThreshold)
        {
            // Получаем клиентов, удовлетворяющих условию
            var clientsToUpdate = _context.Clients
                .Where(c => c.PhysicalAttribute.Age > ageThreshold)
                .ToList(); // Загружаем в память

            // Обновляем записи
            foreach (var client in clientsToUpdate)
            {
                client.Profession = "Обновленная профессия"; // Пример обновления
            }

            // Сохраняем изменения в базе данных
            _context.SaveChanges();

            // Возвращаем обновленные записи
            return _context.Clients.Where(c => c.PhysicalAttribute.Age > ageThreshold);
        }
    }
}