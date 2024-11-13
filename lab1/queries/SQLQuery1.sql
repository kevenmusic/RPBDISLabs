-- —оздание базы данных
CREATE DATABASE MarriageAgencyDB;
GO

-- »спользование базы данных
USE MarriageAgencyDB;
GO

-- “аблица «наки зодиака
CREATE TABLE ZodiacSigns (
    ZodiacSignId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- уникальные названи€ знаков
    Description NVARCHAR(MAX) DEFAULT 'Ќет описани€' -- значение по умолчанию
);
GO

-- “аблица Ќациональности
CREATE TABLE Nationalities (
    NationalityId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- уникальные названи€ национальностей
    Notes NVARCHAR(MAX) DEFAULT 'Ќет доступных заметок' -- значение по умолчанию
);
GO

-- “аблица личных данных клиентов
CREATE TABLE Clients (
    ClientId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('∆енский', 'ћужской')), -- допустимы только ∆енский или ћужской
    BirthDate DATE CHECK (BirthDate <= GETDATE()), -- дата рождени€ не может быть в будущем
    ZodiacSignId INT FOREIGN KEY REFERENCES ZodiacSigns(ZodiacSignId) ON DELETE SET NULL ON UPDATE CASCADE,
    NationalityId INT FOREIGN KEY REFERENCES Nationalities(NationalityId) ON DELETE SET NULL ON UPDATE CASCADE,
    Profession NVARCHAR(100) DEFAULT 'Ѕезработный', -- значение по умолчанию
    ClientPhoto NVARCHAR(MAX) -- исправлено: добавлена зап€та€
);
GO

-- “аблица контактов клиентов
CREATE TABLE Contacts (
    ClientId INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientId) ON DELETE CASCADE ON UPDATE CASCADE,
    Address NVARCHAR(255),
    Phone NVARCHAR(20) UNIQUE, -- уникальный номер телефона
    PassportData NVARCHAR(100) UNIQUE -- уникальные паспортные данные
);
GO

-- “аблица физической информации о клиентах
CREATE TABLE PhysicalAttributes (
    ClientId INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientId) ON DELETE CASCADE ON UPDATE CASCADE,
    Age INT CHECK (Age >= 18), -- минимальный возраст 18
    Height DECIMAL(5,2) CHECK (Height >= 0), -- рост не может быть отрицательным
    Weight DECIMAL(5,2) CHECK (Weight >= 0), -- вес не может быть отрицательным
    ChildrenCount INT CHECK (ChildrenCount >= 0), -- количество детей не может быть отрицательным
    MaritalStatus NVARCHAR(50) DEFAULT 'ќдинокий', -- значение по умолчанию
    BadHabits NVARCHAR(MAX),
    Hobbies NVARCHAR(MAX)
);
GO

-- “аблица дополнительных услуг
CREATE TABLE AdditionalServices (
    AdditionalServiceId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE, -- уникальные названи€ услуг
    Description NVARCHAR(MAX) DEFAULT 'Ќет описани€', -- значение по умолчанию
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0) -- цена должна быть положительной
);
GO

-- “аблица сотрудников
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(100) DEFAULT 'Ќеизвестный', -- значение по умолчанию
    BirthDate DATE CHECK (BirthDate <= GETDATE()) -- дата рождени€ не может быть в будущем
);
GO

-- “аблица оказанных услуг (основные услуги клиентам)
CREATE TABLE Services (
    ServiceId INT PRIMARY KEY IDENTITY(1,1),  -- уникальный идентификатор записи оказанной услуги
    AdditionalServiceId INT NOT NULL FOREIGN KEY REFERENCES AdditionalServices(AdditionalServiceId) ON DELETE CASCADE ON UPDATE CASCADE,  -- дополнительна€ услуга
    ClientId INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientId) ON DELETE CASCADE ON UPDATE CASCADE,  -- клиент, которому оказана услуга
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId) ON DELETE CASCADE ON UPDATE CASCADE,  -- сотрудник, оказавший услугу
    Date DATE NOT NULL,  -- дата оказани€ услуги
    Cost DECIMAL(10, 2) NOT NULL CHECK (Cost >= 0)  -- стоимость услуги
);
GO
