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

            // ���������� �����������
            services.AddMemoryCache();

            // ���������� ��������� ������
            services.AddDistributedMemoryCache();
            services.AddSession(); 

            // ��������� ������������
            services.AddScoped<ICachedClientsService, CachedClientsService>();
            services.AddScoped<ICachedZodiacSignsService, CachedZodiacSignsService>();
            services.AddScoped<ICachedNationalitiesService, CachedNationalitiesService>();
            services.AddScoped<ICachedAdditionalServicesService, CachedAdditionalServicesService>();
            services.AddScoped<ICachedContactsService, CachedContactsService>();
            services.AddScoped<ICachedEmployeesService, CachedEmployeesService>();
            services.AddScoped<ICachedPhysicalAttributesService, CachedPhysicalAttributesService>();
            services.AddScoped<ICachedServicesService, CachedServicesService>();

            //������������� MVC - ���������
            //services.AddControllersWithViews();
            var app = builder.Build();

            // ��������� ��������� ����������� ������
            app.UseStaticFiles();

            // ��������� middleware ��� ������ � ��������
            app.UseSession();

            // ��������� ����������� ��������� middleware �� ������������� ���� ������ � ���������� �� �������������
            app.UseDbInitializer();

            // ����� ���������� � �������
            app.Map("/info", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ������������ ������ ��� ������ 
                    string strResponse = "<HTML><HEAD><TITLE>����������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>����������:</H1>";
                    strResponse += "<BR> ������: " + context.Request.Host;
                    strResponse += "<BR> ����: " + context.Request.PathBase;
                    strResponse += "<BR> ��������: " + context.Request.Protocol;
                    strResponse += "<BR><A href='/'>�������</A></BODY></HTML>";
                    // ����� ������
                    await context.Response.WriteAsync(strResponse);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "��������������"
            app.Map("/nationalities", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    //��������� � �������
                    ICachedNationalitiesService cachedNationalitiesService = context.RequestServices.GetService<ICachedNationalitiesService>();
                    IEnumerable<Nationality> nationalities = cachedNationalitiesService.GetNationalities(20);
                    string HtmlString = "<HTML><HEAD><TITLE>��������������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ ���������������</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� ��������������</TH>";
                    HtmlString += "<TH>�������� ��������������</TH>";
                    HtmlString += "<TH>����������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "����� �������"
            app.Map("/zodiacSigns", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ��������� � �������
                    ICachedZodiacSignsService cachedZodiacSignsService = context.RequestServices.GetService<ICachedZodiacSignsService>();
                    IEnumerable<ZodiacSign> zodiacSigns = cachedZodiacSignsService.GetZodiacSigns(20);
                    string HtmlString = "<HTML><HEAD><TITLE>����� �������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ ������ �������</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� �����</TH>";
                    HtmlString += "<TH>�������� �����</TH>";
                    HtmlString += "</TR>";
                    foreach (var zodiacSign in zodiacSigns)
                    {
                        HtmlString += "<TR>";
                        HtmlString += "<TD>" + zodiacSign.ZodiacSignId + "</TD>";
                        HtmlString += "<TD>" + zodiacSign.Name + "</TD>";
                        HtmlString += "</TR>";
                    }
                    HtmlString += "</TABLE>";
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "���������� ��������"
            app.Map("/physicalAttributes", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ��������� � �������
                    ICachedPhysicalAttributesService cachedPhysicalAttributeService = context.RequestServices.GetService<ICachedPhysicalAttributesService>();
                    IEnumerable<PhysicalAttribute> physicalAttributes = cachedPhysicalAttributeService.GetPhysicalAttributes(20);
                    string HtmlString = "<HTML><HEAD><TITLE>���������� ��������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ ���������� ���������</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� �������</TH>";
                    HtmlString += "<TH>�������</TH>";
                    HtmlString += "<TH>����</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>���������� �����</TH>";
                    HtmlString += "<TH>�������� ���������</TH>";
                    HtmlString += "<TH>������� ��������</TH>";
                    HtmlString += "<TH>�����</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "��������"
            app.Map("/contacts", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ��������� � �������
                    ICachedContactsService cachedContactService = context.RequestServices.GetService<ICachedContactsService>();
                    IEnumerable<Contact> contacts = cachedContactService.GetContacts(20);
                    string HtmlString = "<HTML><HEAD><TITLE>��������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ ���������</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� �������</TH>";
                    HtmlString += "<TH>�����</TH>";
                    HtmlString += "<TH>�������</TH>";
                    HtmlString += "<TH>���������� ������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "�������������� ������"
            app.Map("/additionalServices", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ��������� � �������
                    ICachedAdditionalServicesService cachedAdditionalServiceService = context.RequestServices.GetService<ICachedAdditionalServicesService>();
                    IEnumerable<AdditionalService> additionalServices = cachedAdditionalServiceService.GetAdditionalServices(20);
                    string HtmlString = "<HTML><HEAD><TITLE>�������������� ������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ �������������� �����</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� ������</TH>";
                    HtmlString += "<TH>��������</TH>";
                    HtmlString += "<TH>��������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "����������"
            app.Map("/employees", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ��������� � �������
                    ICachedEmployeesService cachedEmployeeService = context.RequestServices.GetService<ICachedEmployeesService>();
                    IEnumerable<Employee> employees = cachedEmployeeService.GetEmployees(20);
                    string HtmlString = "<HTML><HEAD><TITLE>����������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ �����������</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� ����������</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>�������</TH>";
                    HtmlString += "<TH>���������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            app.Map("/searchEmployees", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    string employeeName = string.Empty;

                    // ���������, �������� �� �������� employeeName � �������
                    if (!string.IsNullOrEmpty(context.Request.Query["employeeName"]))
                    {
                        employeeName = context.Request.Query["employeeName"];
                        // ��������� employeeName � ������
                        context.Session.SetString("employeeName", employeeName);
                    }
                    // ���� employeeName ��� ��������� � ������, ��������� ���
                    else if (context.Session.Keys.Contains("employeeName"))
                    {
                        employeeName = context.Session.GetString("employeeName");
                    }
                    // ��������� �������� ���� ��� �����������, ��������������� � ������ ������, ���� ����������
                    ICachedEmployeesService cachedEmployeesService = context.RequestServices.GetService<ICachedEmployeesService>();
                    IEnumerable<Employee> employees = cachedEmployeesService.GetEmployees(30);

                    string HtmlString = "<HTML><HEAD><TITLE>����������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ ����������� �� �����</H1>" +
                    "<BODY><FORM action ='/searchEmployees' method='get'>" +
                    "��� ����������:<BR><INPUT type = 'text' name = 'employeeName' value = '" + employeeName + "'>" +
                    "<BR><BR><INPUT type ='submit' value='��������� � ������ � ������� ����������� � �������� ������'></FORM>" +
                    "<TABLE BORDER=1>";

                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� ����������</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>�������</TH>";
                    HtmlString += "<TH>��������</TH>";
                    HtmlString += "<TH>���������</TH>";
                    HtmlString += "</TR>";
                    // ���������� ����������� �� �����
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    await context.Response.WriteAsync(HtmlString);
                });
            });


            // ����� ������������ ���������� �� ������� ���� ������ "������"
            app.Map("/services", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    // ��������� � �������
                    ICachedAdditionalServicesService cachedAdditionalServiceService = context.RequestServices.GetService<ICachedAdditionalServicesService>();
                    cachedAdditionalServiceService.AddAdditionalServices("AdditionalServices20", 1000);
                    ICachedClientsService cachedClientsService = context.RequestServices.GetService<ICachedClientsService>();
                    cachedClientsService.AddClients("Clients20", 1000);
                    ICachedEmployeesService cachedEmployeeService = context.RequestServices.GetService<ICachedEmployeesService>();
                    cachedEmployeeService.AddEmployees("Employees20", 1000);
                    ICachedServicesService cachedServicesService = context.RequestServices.GetService<ICachedServicesService>();
                    IEnumerable<Service> services = cachedServicesService.GetServices(20);
                    string HtmlString = "<HTML><HEAD><TITLE>������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ �����</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� ������</TH>";
                    HtmlString += "<TH>�������������� ������</TH>";
                    HtmlString += "<TH>������</TH>";
                    HtmlString += "<TH>���������</TH>";
                    HtmlString += "<TH>����</TH>";
                    HtmlString += "<TH>���������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
                    await context.Response.WriteAsync(HtmlString);
                });
            });

            // ����� ������������ ���������� �� ������� ���� ������ "�������"
            app.Map("/clients", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    //��������� � �������
                    ICachedNationalitiesService cachedNationalitiesService = context.RequestServices.GetService<ICachedNationalitiesService>();
                    cachedNationalitiesService.AddNationalities("Nationalities20", 1000);
                    ICachedZodiacSignsService zodiacSignService = context.RequestServices.GetService<ICachedZodiacSignsService>();
                    zodiacSignService.AddZodiacSigns("ZodiacSigns20", 1000);
                    ICachedClientsService cachedClientsService = context.RequestServices.GetService<ICachedClientsService>();
                    IEnumerable<Client> clients = cachedClientsService.GetClients(20);
                    string HtmlString = "<HTML><HEAD><TITLE>�������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ ��������</H1>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� �������</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>�������</TH>";
                    HtmlString += "<TH>��������</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>���� ��������</TH>";
                    HtmlString += "<TH>���� �������</TH>";
                    HtmlString += "<TH>��������������</TH>";
                    HtmlString += "<TH>���������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    // ����� ������
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
                    string HtmlString = "<HTML><HEAD><TITLE>�������</TITLE></HEAD>" +
                    "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                    "<BODY><H1>������ �������� �� �����</H1>" +
                    "<BODY><FORM action ='/searchClients' method='GET'>" +
                    "���:<BR><INPUT type = 'text' name = 'clientName' value = " + clientName + ">" +
                    "<BR><BR><INPUT type ='submit' value='��������� � cookies � ������� �������� � �������� ������'></FORM>" +
                    "<TABLE BORDER=1>";
                    HtmlString += "<TR>";
                    HtmlString += "<TH>��� �������</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>�������</TH>";
                    HtmlString += "<TH>��������</TH>";
                    HtmlString += "<TH>���</TH>";
                    HtmlString += "<TH>���� ��������</TH>";
                    HtmlString += "<TH>���� �������</TH>";
                    HtmlString += "<TH>��������������</TH>";
                    HtmlString += "<TH>���������</TH>";
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
                    HtmlString += "<BR><A href='/'>�������</A></BR>";
                    HtmlString += "</BODY></HTML>";

                    await context.Response.WriteAsync(HtmlString);
                });
            });

            app.Run((context) =>
            {
                // ����������� ������ ��� ���� ������
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

                string HtmlString = "<HTML><HEAD><TITLE>�������</TITLE></HEAD>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                "<BODY><H1>�������</H1>";
                HtmlString += "<H2>������ �������� � ��� �������</H2>";
                HtmlString += "<BR><A href='/'>�������</A></BR>";
                HtmlString += "<BR><A href='/clients'>�������</A></BR>";
                HtmlString += "<BR><A href='/searchClients'>����� �� ��������</A></BR>";
                HtmlString += "<BR><A href='/employees'>����������</A></BR>";
                HtmlString += "<BR><A href='/searchEmployees'>����� �� �����������</A></BR>";
                HtmlString += "<BR><A href='/services'>������</A></BR>";
                HtmlString += "<BR><A href='/additionalServices'>��������������� ������</A></BR>";
                HtmlString += "<BR><A href='/contacts'>��������</A></BR>";
                HtmlString += "<BR><A href='/physicalAttributes'>���������� ��������</A></BR>";
                HtmlString += "<BR><A href='/zodiacSigns'>����� �������</A></BR>";
                HtmlString += "<BR><A href='/nationalities'>��������������</A></BR>";
                HtmlString += "<BR><A href='/info'>���������� � �������</A></BR>";
                HtmlString += "</BODY></HTML>";

                return context.Response.WriteAsync(HtmlString);
            });

            app.Run();
        }
    }
}