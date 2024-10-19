using DataLayer.Data;
using DataLayer.Models;
using MarriageAgency.Infrastructure;
using MarriageAgency.Middleware;
using MarriageAgency.Services.AdditionalServicesService;
using MarriageAgency.Services.ClientsService;
using MarriageAgency.Services.ContactsService;
using MarriageAgency.Services.EmployeesService;
using MarriageAgency.Services.NationalitiesService;
using MarriageAgency.Services.PhysicalAttributesService;
using MarriageAgency.Services.ServicesService;
using MarriageAgency.Services.ZodiacSignsService;
using Microsoft.EntityFrameworkCore;

namespace MarriageAgency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MarriageAgencyContext>(options => options.UseSqlServer(connectionString));

            // добавление кэширования
            services.AddMemoryCache();

            // добавление поддержки сессии
            services.AddDistributedMemoryCache();
            services.AddSession(); 

            // внедрение зависимостей
            services.AddScoped<ICachedClientsService, CachedClientsService>();
            services.AddScoped<ICachedZodiacSignsService, CachedZodiacSignsService>();
            services.AddScoped<ICachedNationalitiesService, CachedNationalitiesService>();
            services.AddScoped<ICachedAdditionalServicesService, CachedAdditionalServicesService>();
            services.AddScoped<ICachedContactsService, CachedContactsService>();
            services.AddScoped<ICachedEmployeesService, CachedEmployeesService>();
            services.AddScoped<ICachedPhysicalAttributesService, CachedPhysicalAttributesService>();
            services.AddScoped<ICachedServicesService, CachedServicesService>();

            //Использование MVC - отключено
            //services.AddControllersWithViews();
            var app = builder.Build();

            // добавляем поддержку статических файлов
            app.UseStaticFiles();

            // добавляем middleware для работы с сессиями
            app.UseSession();

            // добавляем собственный компонент middleware по инициализации базы данных и производим ее инициализацию
            app.UseDbInitializer();

            // Вывод информации о клиенте
            app.Map("/info", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // Формирование строки для вывода 
                    string strResponse = "<HTML><HEAD><TITLE>Информация</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Информация:</H1>";
                    strResponse += "<BR> Сервер: " + context.Request.Host;
                    strResponse += "<BR> Путь: " + context.Request.PathBase;
                    strResponse += "<BR> Протокол: " + context.Request.Protocol;
                    strResponse += "<BR><A href='/'>Главная</A></BODY></HTML>";
                    // Вывод данных
                    await context.Response.WriteAsync(strResponse);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Национальности"
            app.Map("/nationalities", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    //обращение к сервису
                    ICachedNationalitiesService cachedNationalitiesService = context.RequestServices.GetService<ICachedNationalitiesService>();
                    IEnumerable<Nationality> nationalities = cachedNationalitiesService.GetNationalities(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Национальности</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список национальностей</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Национальности</TH>";
                    HtmlString += "<TH>Название национальности</TH>";
                    HtmlString += "<TH>Примечания</TH>";
                    HtmlString += "</TR>";
                    foreach (var nationality in nationalities)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + nationality.NationalityId + "</TD>";
                        HtmlString += "<TD>" + nationality.Name + "</TD>";
                        HtmlString += "<TD>" + nationality.Notes + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Знаки зодиака"
            app.Map("/zodiacSigns", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // обращение к сервису
                    ICachedZodiacSignsService cachedZodiacSignsService = context.RequestServices.GetService<ICachedZodiacSignsService>();
                    IEnumerable<ZodiacSign> zodiacSigns = cachedZodiacSignsService.GetZodiacSigns(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Знаки зодиака</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список знаков зодиака</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Знака</TH>";
                    HtmlString += "<TH>Название знака</TH>";
                    HtmlString += "</TR>";
                    foreach (var zodiacSign in zodiacSigns)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + zodiacSign.ZodiacSignId + "</TD>";
                        HtmlString += "<TD>" + zodiacSign.Name + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Физические атрибуты"
            app.Map("/physicalAttributes", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // обращение к сервису
                    ICachedPhysicalAttributesService cachedPhysicalAttributeService = context.RequestServices.GetService<ICachedPhysicalAttributesService>();
                    IEnumerable<PhysicalAttribute> physicalAttributes = cachedPhysicalAttributeService.GetPhysicalAttributes(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Физические атрибуты</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список физических атрибутов</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Клиента</TH>";
                    HtmlString += "<TH>Возраст</TH>";
                    HtmlString += "<TH>Рост</TH>";
                    HtmlString += "<TH>Вес</TH>";
                    HtmlString += "<TH>Количество детей</TH>";
                    HtmlString += "<TH>Семейное положение</TH>";
                    HtmlString += "<TH>Вредные привычки</TH>";
                    HtmlString += "<TH>Хобби</TH>";
                    HtmlString += "</TR>";
                    foreach (var attribute in physicalAttributes)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + attribute.ClientId + "</TD>";
                        HtmlString += "<TD>" + attribute.Age + "</TD>";
                        HtmlString += "<TD>" + attribute.Height + "</TD>";
                        HtmlString += "<TD>" + attribute.Weight + "</TD>";
                        HtmlString += "<TD>" + attribute.ChildrenCount + "</TD>";
                        HtmlString += "<TD>" + attribute.MaritalStatus + "</TD>";
                        HtmlString += "<TD>" + attribute.BadHabits + "</TD>";
                        HtmlString += "<TD>" + attribute.Hobbies + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Контакты"
            app.Map("/contacts", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // обращение к сервису
                    ICachedContactsService cachedContactService = context.RequestServices.GetService<ICachedContactsService>();
                    IEnumerable<Contact> contacts = cachedContactService.GetContacts(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Контакты</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список контактов</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Клиента</TH>";
                    HtmlString += "<TH>Адрес</TH>";
                    HtmlString += "<TH>Телефон</TH>";
                    HtmlString += "<TH>Паспортные данные</TH>";
                    HtmlString += "</TR>";
                    foreach (var contact in contacts)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + contact.ClientId + "</TD>";
                        HtmlString += "<TD>" + contact.Address + "</TD>";
                        HtmlString += "<TD>" + contact.Phone + "</TD>";
                        HtmlString += "<TD>" + contact.PassportData + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Дополнительные услуги"
            app.Map("/additionalServices", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // обращение к сервису
                    ICachedAdditionalServicesService cachedAdditionalServiceService = context.RequestServices.GetService<ICachedAdditionalServicesService>();
                    IEnumerable<AdditionalService> additionalServices = cachedAdditionalServiceService.GetAdditionalServices(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Дополнительные услуги</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список дополнительных услуг</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Услуги</TH>";
                    HtmlString += "<TH>Название</TH>";
                    HtmlString += "<TH>Описание</TH>";
                    HtmlString += "</TR>";
                    foreach (var service in additionalServices)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + service.AdditionalServiceId + "</TD>";
                        HtmlString += "<TD>" + service.Name + "</TD>";
                        HtmlString += "<TD>" + service.Description + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Сотрудники"
            app.Map("/employees", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // обращение к сервису
                    ICachedEmployeesService cachedEmployeeService = context.RequestServices.GetService<ICachedEmployeesService>();
                    IEnumerable<Employee> employees = cachedEmployeeService.GetEmployees(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Сотрудники</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список сотрудников</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Сотрудника</TH>";
                    HtmlString += "<TH>Имя</TH>";
                    HtmlString += "<TH>Фамилия</TH>";
                    HtmlString += "<TH>Должность</TH>";
                    HtmlString += "</TR>";
                    foreach (var employee in employees)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + employee.EmployeeId + "</TD>";
                        HtmlString += "<TD>" + employee.FirstName + "</TD>";
                        HtmlString += "<TD>" + employee.LastName + "</TD>";
                        HtmlString += "<TD>" + employee.Position + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            app.Map("/searchEmployees", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string employeeName = string.Empty;

                    // Проверяем, передано ли значение employeeName в запросе
                    if (!string.IsNullOrEmpty(context.Request.Query["employeeName"]))
                    {
                        employeeName = context.Request.Query["employeeName"];
                        // Сохраняем employeeName в сессии
                        context.Session.SetString("employeeName", employeeName);
                    }
                    // Если employeeName уже сохранено в сессии, извлекаем его
                    else if (context.Session.Keys.Contains("employeeName"))
                    {
                        employeeName = context.Session.GetString("employeeName");
                    }
                    // Получение сервисов кэша для сотрудников, национальностей и других данных, если необходимо
                    ICachedEmployeesService cachedEmployeesService = context.RequestServices.GetService<ICachedEmployeesService>();
                    IEnumerable<Employee> employees = cachedEmployeesService.GetEmployees(30);

                    string HtmlString = "<HTML><HEAD><TITLE>Сотрудники</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список сотрудников по имени</H1>" +
                    "<BODY><FORM action ='/searchEmployees' method='get'>" +
                    "Имя сотрудника:<BR><INPUT type = 'text' name = 'employeeName' value = '" + employeeName + "'>" +
                    "<BR><BR><INPUT type ='submit' value='Сохранить в сессию и вывести сотрудников с заданным именем'></FORM>" +
                    "<TABLE BORDER=1>";

                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Сотрудника</TH>";
                    HtmlString += "<TH>Имя</TH>";
                    HtmlString += "<TH>Фамилия</TH>";
                    HtmlString += "<TH>Отчество</TH>";
                    HtmlString += "<TH>Должность</TH>";
                    HtmlString += "</TR>";
                    // Фильтрация сотрудников по имени
                    foreach (var employee in employees.Where(e => e.FirstName.Trim() == employeeName))
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + employee.EmployeeId + "</TD>";
                        HtmlString += "<TD>" + employee.FirstName + "</TD>";
                        HtmlString += "<TD>" + employee.LastName + "</TD>";
                        HtmlString += "<TD>" + employee.MiddleName + "</TD>";
                        HtmlString += "<TD>" + employee.Position + "</TD>";
                        HtmlString += "</TR>";
                    }

                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    await context.Response.WriteAsync(HtmlString);
                });
            });


            // Вывод кэшированной информации из таблицы базы данных "Услуги"
            app.Map("/services", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // обращение к сервису
                    ICachedAdditionalServicesService cachedAdditionalServiceService = context.RequestServices.GetService<ICachedAdditionalServicesService>();
                    cachedAdditionalServiceService.AddAdditionalServices("AdditionalServices20", 1000);
                    ICachedClientsService cachedClientsService = context.RequestServices.GetService<ICachedClientsService>();
                    cachedClientsService.AddClients("Clients20", 1000);
                    ICachedEmployeesService cachedEmployeeService = context.RequestServices.GetService<ICachedEmployeesService>();
                    cachedEmployeeService.AddEmployees("Employees20", 1000);
                    ICachedServicesService cachedServicesService = context.RequestServices.GetService<ICachedServicesService>();
                    IEnumerable<Service> services = cachedServicesService.GetServices(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Услуги</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список услуг</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Услуги</TH>";
                    HtmlString += "<TH>Дополнительная услуга</TH>";
                    HtmlString += "<TH>Клиент</TH>";
                    HtmlString += "<TH>Сотрудник</TH>";
                    HtmlString += "<TH>Дата</TH>";
                    HtmlString += "<TH>Стоимость</TH>";
                    HtmlString += "</TR>";
                    foreach (var service in services)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + service.ServiceId + "</TD>";
                        HtmlString += "<TD>" + service.AdditionalService.Name + "</TD>";
                        HtmlString += "<TD>" + service.Client.FirstName + "</TD>";
                        HtmlString += "<TD>" + service.Employee.FirstName + "</TD>";
                        HtmlString += "<TD>" + service.Date + "</TD>";
                        HtmlString += "<TD>" + service.Cost + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // Вывод кэшированной информации из таблицы базы данных "Клиенты"
            app.Map("/clients", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    //обращение к сервису
                    ICachedNationalitiesService cachedNationalitiesService = context.RequestServices.GetService<ICachedNationalitiesService>();
                    cachedNationalitiesService.AddNationalities("Nationalities20", 1000);
                    ICachedZodiacSignsService zodiacSignService = context.RequestServices.GetService<ICachedZodiacSignsService>();
                    zodiacSignService.AddZodiacSigns("ZodiacSigns20", 1000);
                    ICachedClientsService cachedClientsService = context.RequestServices.GetService<ICachedClientsService>();
                    IEnumerable<Client> clients = cachedClientsService.GetClients(20);
                    string HtmlString = "<HTML><HEAD><TITLE>Клиенты</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список клиентов</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Клиента</TH>";
                    HtmlString += "<TH>Имя</TH>";
                    HtmlString += "<TH>Фамилия</TH>";
                    HtmlString += "<TH>Отчество</TH>";
                    HtmlString += "<TH>Пол</TH>";
                    HtmlString += "<TH>Дата рождения</TH>";
                    HtmlString += "<TH>Знак зодиака</TH>";
                    HtmlString += "<TH>Национальность</TH>";
                    HtmlString += "<TH>Профессия</TH>";
                    HtmlString += "</TR>";
                    foreach (var client in clients)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + client.ClientId + "</TD>";
                        HtmlString += "<TD>" + client.FirstName + "</TD>";
                        HtmlString += "<TD>" + client.LastName + "</TD>";
                        HtmlString += "<TD>" + client.MiddleName + "</TD>";
                        HtmlString += "<TD>" + client.Gender + "</TD>";
                        HtmlString += "<TD>" + client.BirthDate + "</TD>";
                        HtmlString += "<TD>" + client.ZodiacSign?.Name + "</TD>";
                        HtmlString += "<TD>" + client.Nationality?.Name + "</TD>";
                        HtmlString += "<TD>" + client.Profession + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // Вывод данных
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            app.Map("/searchClients", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string clientName;
                    context.Request.Cookies.TryGetValue("clientName", out clientName);
                    ICachedNationalitiesService cachedNationalitiesService = context.RequestServices.GetService<ICachedNationalitiesService>();
                    cachedNationalitiesService.AddNationalities("Nationalities20", 1000);
                    ICachedZodiacSignsService zodiacSignService = context.RequestServices.GetService<ICachedZodiacSignsService>();
                    zodiacSignService.AddZodiacSigns("ZodiacSigns20", 1000);
                    ICachedClientsService cachedClientsService = context.RequestServices.GetService<ICachedClientsService>();
                    IEnumerable<Client> clients = cachedClientsService.GetClients(30);
                    string HtmlString = "<HTML><HEAD><TITLE>Клиенты</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>Список клиентов по имени</H1>" +
                    "<BODY><FORM action ='/searchClients' method='GET'>" +
                    "Имя:<BR><INPUT type = 'text' name = 'clientName' value = " + clientName + ">" +
                    "<BR><BR><INPUT type ='submit' value='Сохранить в cookies и вывести клиентов с заданным именем'></FORM>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>Код Клиента</TH>";
                    HtmlString += "<TH>Имя</TH>";
                    HtmlString += "<TH>Фамилия</TH>";
                    HtmlString += "<TH>Отчество</TH>";
                    HtmlString += "<TH>Пол</TH>";
                    HtmlString += "<TH>Дата рождения</TH>";
                    HtmlString += "<TH>Знак зодиака</TH>";
                    HtmlString += "<TH>Национальность</TH>";
                    HtmlString += "<TH>Профессия</TH>";
                    HtmlString += "</TR>";

                    clientName = context.Request.Query["clientName"];
                    if (!string.IsNullOrEmpty(clientName))
                    {
                        context.Response.Cookies.Append("clientName", clientName);
                    }
                    foreach (var client in clients.Where(e => e.FirstName.Trim().ToLower() == clientName?.ToLower()))
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + client.ClientId + "</TD>";
                        HtmlString += "<TD>" + client.FirstName + "</TD>";
                        HtmlString += "<TD>" + client.LastName + "</TD>";
                        HtmlString += "<TD>" + client.MiddleName + "</TD>";
                        HtmlString += "<TD>" + client.Gender + "</TD>";
                        HtmlString += "<TD>" + client.BirthDate + "</TD>";
                        HtmlString += "<TD>" + client.ZodiacSign?.Name + "</TD>";
                        HtmlString += "<TD>" + client.Nationality?.Name + "</TD>";
                        HtmlString += "<TD>" + client.Profession + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>Главная</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    await context.Response.WriteAsync(HtmlString);
                });
            });

            app.Run((context) =>
            {
                // Кэширование данных для всех таблиц
                ICachedNationalitiesService cachedNationalitiesService = context.RequestServices.GetService<ICachedNationalitiesService>();
                cachedNationalitiesService.AddNationalities("Nationalities20", 20);

                ICachedClientsService cachedClientsService = context.RequestServices.GetService<ICachedClientsService>();
                cachedClientsService.AddClients("Clients20", 1000);

                ICachedEmployeesService cachedEmployeesService = context.RequestServices.GetService<ICachedEmployeesService>();
                cachedEmployeesService.AddEmployees("Employees20", 100);

                ICachedServicesService cachedServicesService = context.RequestServices.GetService<ICachedServicesService>();
                cachedServicesService.AddServices("Services20", 50);

                ICachedAdditionalServicesService cachedAdditionalServicesService = context.RequestServices.GetService<ICachedAdditionalServicesService>();
                cachedAdditionalServicesService.AddAdditionalServices("AdditionalServices20", 30);

                ICachedContactsService cachedContactsService = context.RequestServices.GetService<ICachedContactsService>();
                cachedContactsService.AddContacts("Contacts20", 500);

                ICachedPhysicalAttributesService cachedPhysicalAttributesService = context.RequestServices.GetService<ICachedPhysicalAttributesService>();
                cachedPhysicalAttributesService.AddPhysicalAttributes("PhysicalAttributes20", 200);

                ICachedZodiacSignsService cachedZodiacSignsService = context.RequestServices.GetService<ICachedZodiacSignsService>();
                cachedZodiacSignsService.AddZodiacSigns("ZodiacSigns20", 12);

                string HtmlString = "<HTML><HEAD><TITLE>Главная</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>Главная</H1>";
                HtmlString += "<H2>Данные записаны в кэш сервера</H2>";
                HtmlString += "<BR><A href='/'>Главная</A></BR>";
                HtmlString += "<BR><A href='/clients'>Клиенты</A></BR>";
                HtmlString += "<BR><A href='/searchClients'>Поиск по клиентам</A></BR>";
                HtmlString += "<BR><A href='/employees'>Сотрудники</A></BR>";
                HtmlString += "<BR><A href='/searchEmployees'>Поиск по сотрудникам</A></BR>";
                HtmlString += "<BR><A href='/services'>Услуги</A></BR>";
                HtmlString += "<BR><A href='/additionalServices'>Дополонительные услуги</A></BR>";
                HtmlString += "<BR><A href='/contacts'>Контакты</A></BR>";
                HtmlString += "<BR><A href='/physicalAttributes'>Физические атрибуты</A></BR>";
                HtmlString += "<BR><A href='/zodiacSigns'>Знаки зодиака</A></BR>";
                HtmlString += "<BR><A href='/nationalities'>Национальности</A></BR>";
                HtmlString += "<BR><A href='/info'>Информация о клиенте</A></BR>";
                HtmlString += "</BODY></HTML>";

                return context.Response.WriteAsync(HtmlString);
            });

            app.Run();
        }
    }
}