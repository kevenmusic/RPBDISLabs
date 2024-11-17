using Azure;
using MarriageAgency.Data;
using MarriageAgency.Models;
using MarriageAgency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarriageAgency.Controllers
{
    [Route("api/[controller]")]
    public class ServicesController(MarriageAgencyContext context) : Controller
    {
        private readonly MarriageAgencyContext _context = context;

        /// <summary>
        /// Получение списка всех услуг
        /// </summary>
        // GET api/values
        [HttpGet]
        [Produces("application/json")]
        public List<ServicesViewModel> Get()
        {
            IQueryable<ServicesViewModel> svm = _context.Services
                .Include(t => t.Client)
                .Include(f => f.Employee)
                .Include(f => f.AdditionalService)
                .OrderBy(o => o.ServiceId)  // Добавлена сортировка по ServiceId
                .Select(o => new ServicesViewModel
                {
                    ServiceId = o.ServiceId,
                    Date = o.Date,
                    Cost = o.Cost,
                    AdditionalServiceId = o.AdditionalServiceId,
                    EmployeeId = o.EmployeeId,
                    ClientId = o.ClientId,
                    AdditionalServiceName = o.AdditionalService.Name,
                    EmployeeName = o.Employee.FirstName,
                    ClientName = o.Client.FirstName,
                });
            return svm.Take(500).ToList();
        }


        /// <summary>
        /// Получение списка услуг, удовлетворяющих заданному условию
        /// </summary>
        /// <remarks>
        /// Описание параметров
        /// </remarks>
        /// <param name="employeeId">Код сотрудника</param>
        /// <param name="clientId">Код клиента</param>
        /// <param name="additionalServiceId">Код дополнительной услуги</param>
        /// <returns>JSON</returns>
        [HttpGet("FilteredServices")]
        [Produces("application/json")]
        public List<ServicesViewModel> GetFilteredServices(int employeeId, int clientId, int additionalServiceId)
        {
            IQueryable<ServicesViewModel> svm = _context.Services
                .Include(t => t.Client)
                .Include(f => f.Employee)
                .Include(f => f.AdditionalService)
                .Select(o => new ServicesViewModel
                {
                    ServiceId = o.ServiceId,
                    Date = o.Date,
                    Cost = o.Cost,
                    AdditionalServiceId = o.AdditionalServiceId,
                    EmployeeId = o.EmployeeId,
                    ClientId = o.ClientId,
                    AdditionalServiceName = o.AdditionalService.Name,
                    EmployeeName = o.Employee.FirstName,
                    ClientName = o.Client.FirstName,
                });

            if (employeeId > 0)
            {
                svm = svm.Where(op => op.EmployeeId == employeeId);
            }
            if (clientId > 0)
            {
                svm = svm.Where(op => op.ClientId == clientId);
            }
            if (additionalServiceId > 0)
            {
                svm = svm.Where(op => op.AdditionalServiceId == additionalServiceId);
            }

            // Добавлена сортировка по ServiceId
            return svm.Take(500).OrderBy(o => o.ServiceId).ToList();
        }


        /// <summary>
        /// Список клиентов
        /// </summary>
        [HttpGet("clients")]
        [Produces("application/json")]
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        [HttpGet("employees")]
        [Produces("application/json")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        /// <summary>
        /// Список дополнительных услуг
        /// </summary>
        [HttpGet("additionalServices")]
        [Produces("application/json")]
        public IEnumerable<AdditionalService> GetAdditionalServices()
        {
            return _context.AdditionalServices.ToList();
        }

        /// <summary>
        /// Получение данных одной услуги
        /// </summary>
        /// <remarks>
        /// Описание параметра
        /// </remarks>
        /// <param name="id">Код услуги</param>
        /// <returns>JSON</returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.ServiceId == id);
            if (service == null)
                return NotFound();
            return new ObjectResult(service);
        }

        /// <summary>
        /// Регистрация новой услуги
        /// </summary>
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest();
            }
            _context.Services.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }

        /// <summary>
        /// Обновление данных одной услуги
        /// </summary>
        /// <remarks>
        /// Объект передается в теле запроса
        /// </remarks>
        /// <param name="service">объект, определяющий услугу</param>
        /// <returns>Статус</returns>
        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest();
            }
            if (!_context.Services.Any(x => x.ServiceId == service.ServiceId))
            {
                return NotFound();
            }
            _context.Update(service);
            _context.SaveChanges();
            return Ok(service);
        }

        /// <summary>
        /// Удаление данных одной услуги
        /// </summary>
        /// <remarks>
        /// Описание параметра
        /// </remarks>
        /// <param name="id">Код услуги</param>
        /// <returns>Статус</returns>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return Ok(service);
        }
    }
}
