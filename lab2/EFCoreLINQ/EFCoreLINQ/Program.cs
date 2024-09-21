using System;
using EFCoreLINQ.Data;
using EFCoreLINQ.Models;
using EFCoreLINQ.Queries;

using var context = new MarriageAgencyContext();
var queries = new Queries(context);

while (true)
{
    Console.Clear();
    Console.WriteLine("|=================================================================================================================|");
    Console.WriteLine("|                                                  Меню                                                           |");
    Console.WriteLine("| Показать всех клиентов - Выборка всех данных из таблицы Client.                                                 |");
    Console.WriteLine("| Фильтрация клиентов по профессии - Выборка клиентов, отфильтрованных по профессии.                              |");
    Console.WriteLine("| Группировка клиентов по профессии - Группировка клиентов по профессии и вывод итоговых данных.                  |");
    Console.WriteLine("| Выборка услуг клиентов - Выборка данных из таблиц Service и Client, связанных отношением один-ко-многим.        |");
    Console.WriteLine("| Фильтрация клиентов по национальности - Выборка клиентов по национальности из связанных таблиц.                 |");
    Console.WriteLine("| Добавить нового клиента - Вставка данных в таблицу Client.                                                      |");
    Console.WriteLine("| Добавить новую услугу - Вставка данных в таблицу Service.                                                       |");
    Console.WriteLine("| Удалить клиента - Удаление данных из таблицы Client по имени.                                                   |");
    Console.WriteLine("| Удалить услугу - Удаление данных из таблицы Service по ID.                                                      |");
    Console.WriteLine("| Обновить записи клиентов - Обновление записей в таблице Client на основе данных из таблицы PhysicalAttribute.   |");
    Console.WriteLine("|=================================================================================================================|");
    Console.Write("Выберите опцию: ");
    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            ShowAllClients(queries);
            break;
        case "2":
            FilterClients(queries);
            break;
        case "3":
            GroupClientsByProfession(queries);
            break;
        case "4":
            ShowClientServices(queries);
            break;
        case "5":
            FilterClientsByNationality(queries);
            break;
        case "6":
            AddNewClient(queries);
            break;
        case "7":
            AddNewService(queries);
            break;
        case "8":
            DeleteClientByName(queries);
            break;
        case "9":
            DeleteService(queries);
            break;
        case "10":
            UpdateClientRecords(queries);
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            break;
    }
}

void ShowAllClients(Queries service)
{
    Console.Clear();
    Console.Write("Введите количество клиентов для показа: ");
    if (!int.TryParse(Console.ReadLine(), out var recordsNumber))
    {
        Console.WriteLine("Некорректное число.");
        return;
    }

    var clients = service.GetAllClients(recordsNumber);
    PrintClientDetails(clients);

    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void FilterClients(Queries service)
{
    Console.Clear();
    Console.Write("Введите профессию для фильтрации (например, Программист): ");
    var professionFilter = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(professionFilter))
    {
        Console.WriteLine("Пожалуйста, введите действительную профессию.");
        Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
        Console.ReadKey();
        return;
    }

    Console.Write("Введите количество клиентов для показа: ");
    if (!int.TryParse(Console.ReadLine(), out var recordsNumber))
    {
        Console.WriteLine("Некорректное число.");
        return;
    }

    var filteredClients = service.FilterClientsByProfession(professionFilter, recordsNumber);
    PrintClientDetails(filteredClients);

    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void GroupClientsByProfession(Queries service)
{
    Console.Clear();
    Console.Write("Введите максимальное количество клиентов на профессию: ");
    if (!int.TryParse(Console.ReadLine(), out var recordsNumber) || recordsNumber <= 0)
    {
        Console.WriteLine("Некорректное число. Попробуйте снова.");
        Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
        Console.ReadKey();
        return;
    }

    var professionGroups = service.GroupClientsByProfession(recordsNumber);

    Console.WriteLine("Группировка клиентов по профессии:");
    foreach (dynamic group in professionGroups)
    {
        Console.WriteLine($"Профессия: {group.Profession}");
        Console.WriteLine($"Количество клиентов: {group.ClientsCount}");
        Console.WriteLine($"Минимальный рост: {group.MinHeight}");
        Console.WriteLine($"Максимальный рост: {group.MaxHeight}");
        Console.WriteLine($"Средний рост: {group.AvgHeight}");

        foreach (var client in group.Clients)
        {
            Console.WriteLine($" - {client.FirstName} {client.LastName}");
        }
        Console.WriteLine();
    }

    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}


void ShowClientServices(Queries service)
{
    Console.Clear();
    Console.Write("Введите количество услуг для показа: ");
    if (!int.TryParse(Console.ReadLine(), out var recordsNumber))
    {
        Console.WriteLine("Некорректное число.");
        return;
    }

    var clientServices = service.GetClientServices(recordsNumber);

    Console.WriteLine("Услуги клиентов:");
    foreach (dynamic clientService in clientServices.ToList())
    {
        Console.WriteLine($"Клиент: {clientService.ClientName}");
        Console.WriteLine($"Услуга: {clientService.ServiceName}");
        Console.WriteLine($"Стоимость: {clientService.Cost}");
        Console.WriteLine($"Дата: {clientService.Date:yyyy-MM-dd}");
        Console.WriteLine();
    }

    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void FilterClientsByNationality(Queries service)
{
    Console.Clear();
    Console.Write("Введите название национальности для фильтрации (например, Русский): ");
    var nationalityFilter = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nationalityFilter))
    {
        Console.WriteLine("Пожалуйста, введите действительную национальность.");
        Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
        Console.ReadKey();
        return;
    }

    Console.Write("Введите количество клиентов для показа: ");
    if (!int.TryParse(Console.ReadLine(), out var recordsNumber))
    {
        Console.WriteLine("Некорректное число.");
        return;
    }

    var filteredClients = service.FilterClientsByNationality(nationalityFilter, recordsNumber);

    Console.WriteLine("Фильтрованные клиенты:");
    foreach (dynamic client in filteredClients)
    {
        Console.WriteLine($"Имя клиента: {client.ClientName}");
        Console.WriteLine($"Национальность: {client.Nationality}");
        Console.WriteLine($"Профессия: {client.Profession}");
        Console.WriteLine();
    }

    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void AddNewClient(Queries service)
{
    Console.Clear();

    Console.Write("Введите имя клиента: ");
    var firstName = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(firstName))
    {
        Console.WriteLine("Имя клиента не может быть пустым.");
        return;
    }

    Console.Write("Введите фамилию клиента: ");
    var lastName = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(lastName))
    {
        Console.WriteLine("Фамилия клиента не может быть пустой.");
        return;
    }

    Console.Write("Введите отчество клиента: ");
    var middleName = Console.ReadLine();

    Console.Write("Введите пол клиента (Мужской/Женский): ");
    var gender = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(gender) || (gender != "Мужской" && gender != "Женский"))
    {
        Console.WriteLine("Пол клиента должен быть 'М' или 'Ж'.");
        return;
    }

    Console.Write("Введите ID национальности клиента: ");
    if (!int.TryParse(Console.ReadLine(), out var nationalityId))
    {
        Console.WriteLine("Некорректный ID национальности.");
        return;
    }

    Console.Write("Введите профессию клиента: ");
    var profession = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(profession))
    {
        Console.WriteLine("Профессия клиента не может быть пустой.");
        return;
    }

    Console.Write("Введите ID знака зодиака клиента: ");
    if (!int.TryParse(Console.ReadLine(), out var zodiacSignId))
    {
        Console.WriteLine("Некорректный ID знака зодиака.");
        return;
    }

    var newClient = new Client
    {
        FirstName = firstName,
        LastName = lastName,
        MiddleName = middleName,
        Gender = gender,
        NationalityId = nationalityId,
        Profession = profession,
        ZodiacSignId = zodiacSignId
    };

    service.AddClient(newClient);

    Console.WriteLine("Клиент успешно добавлен!");
    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void AddNewService(Queries service)
{
    Console.Clear();
    Console.Write("Введите ID клиента, для которого добавляется услуга: ");
    if (!int.TryParse(Console.ReadLine(), out var clientId))
    {
        Console.WriteLine("Некорректный ID клиента.");
        return;
    }

    Console.Write("Введите ID дополнительной услуги: ");
    if (!int.TryParse(Console.ReadLine(), out var additionalServiceId))
    {
        Console.WriteLine("Некорректный ID дополнительной услуги.");
        return;
    }

    Console.Write("Введите дату услуги (в формате YYYY-MM-DD): ");
    if (!DateOnly.TryParse(Console.ReadLine(), out var date))
    {
        Console.WriteLine("Некорректная дата.");
        return;
    }

    Console.Write("Введите стоимость услуги: ");
    if (!decimal.TryParse(Console.ReadLine(), out var cost))
    {
        Console.WriteLine("Некорректная стоимость.");
        return;
    }

    var newService = new Service
    {
        ClientId = clientId,
        AdditionalServiceId = additionalServiceId,
        Date = date,
        Cost = cost
    };

    service.AddService(newService);

    Console.WriteLine("Услуга успешно добавлена!");
    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void DeleteClientByName(Queries service)
{
    Console.Clear();
    Console.Write("Введите имя клиента для удаления: ");
    var firstName = Console.ReadLine();
    Console.Write("Введите фамилию клиента для удаления: ");
    var lastName = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
    {
        Console.WriteLine("Имя и фамилия не могут быть пустыми. Попробуйте еще раз.");
        Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
        Console.ReadKey();
        return;
    }

    service.DeleteClient(firstName, lastName);
    Console.WriteLine("Клиент успешно удален!");
    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}


void DeleteService(Queries service)
{
    Console.Clear();
    Console.Write("Введите ID услуги для удаления: ");
    if (!int.TryParse(Console.ReadLine(), out var serviceId))
    {
        Console.WriteLine("Некорректный ID услуги.");
        return;
    }

    service.DeleteService(serviceId);
    Console.WriteLine("Услуга успешно удалена!");
    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void UpdateClientRecords(Queries service)
{
    Console.Clear();
    service.UpdateClientRecords();

    Console.WriteLine("Записи клиентов успешно обновлены!");
    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
    Console.ReadKey();
}

void PrintClientDetails(IQueryable<Client> clients)
{
    foreach (var client in clients.ToList())
    {
        Console.WriteLine("Детали клиента:");
        Console.WriteLine($"ID клиента: {client.ClientId}");
        Console.WriteLine($"Имя: {client.FirstName}");
        Console.WriteLine($"Фамилия: {client.LastName}");
        Console.WriteLine($"Отчество: {client.MiddleName}");
        Console.WriteLine($"Пол: {client.Gender}");
        Console.WriteLine($"ID национальности: {client.NationalityId}");
        Console.WriteLine($"Профессия: {client.Profession}");
        Console.WriteLine($"ID знака зодиака: {client.ZodiacSignId}");
        Console.WriteLine();
    }
}
