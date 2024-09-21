-- —оздание базы данных
CREATE DATABASE MarriageAgency;
GO

-- »спользование базы данных
USE MarriageAgency;
GO

-- “аблица «наки зодиака
CREATE TABLE ZodiacSigns (
    ZodiacSignID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- уникальные названи€ знаков
    Description NVARCHAR(MAX) DEFAULT 'Ќет описани€' -- значение по умолчанию
);
GO

-- “аблица Ќациональности
CREATE TABLE Nationalities (
    NationalityID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- уникальные названи€ национальностей
    Notes NVARCHAR(MAX) DEFAULT 'Ќет доступных заметок' -- значение по умолчанию
);
GO

-- “аблица личных данных клиентов
CREATE TABLE Clients (
    ClientID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('∆енский', 'ћужской')), -- допустимы только ∆енский или ћужской
    BirthDate DATE CHECK (BirthDate <= GETDATE()), -- дата рождени€ не может быть в будущем
    ZodiacSignID INT FOREIGN KEY REFERENCES ZodiacSigns(ZodiacSignID) ON DELETE SET NULL ON UPDATE CASCADE,
    NationalityID INT FOREIGN KEY REFERENCES Nationalities(NationalityID) ON DELETE SET NULL ON UPDATE CASCADE,
    Profession NVARCHAR(100) DEFAULT 'Ѕезработный' -- значение по умолчанию
);
GO

-- “аблица контактов клиентов
CREATE TABLE Contacts (
    ClientID INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE,
    Address NVARCHAR(255),
    Phone NVARCHAR(20) UNIQUE, -- уникальный номер телефона
    PassportData NVARCHAR(100) UNIQUE -- уникальные паспортные данные
);
GO

-- “аблица физической информации о клиентах
CREATE TABLE PhysicalAttributes (
    ClientID INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE,
    Age INT CHECK (Age >= 18), -- минимальный возраст 18
    Height DECIMAL(5,2) CHECK (Height >= 0), -- рост не может быть отрицательным
    Weight DECIMAL(5,2) CHECK (Weight >= 0), -- вес не может быть отрицательным
    ChildrenCount INT CHECK (ChildrenCount >= 0), -- количество детей не может быть отрицательным
    MaritalStatus NVARCHAR(50) DEFAULT 'ќдинокий', -- значение по умолчанию
    BadHabits NVARCHAR(MAX),
    Hobbies NVARCHAR(MAX)
);
GO

-- “аблица фотографий клиентов (один-к-одному с таблицей клиентов)
CREATE TABLE Photos (
    ClientID INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE, -- св€зь один к одному
    Photo NVARCHAR(MAX) NOT NULL -- ѕуть к фото или двоичные данные
);
GO

-- “аблица дополнительных услуг
CREATE TABLE AdditionalServices (
    AdditionalServiceID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE, -- уникальные названи€ услуг
    Description NVARCHAR(MAX) DEFAULT 'Ќет описани€', -- значение по умолчанию
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0) -- цена должна быть положительной
);
GO

-- “аблица сотрудников
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(100) DEFAULT 'Ќеизвестный', -- значение по умолчанию
    BirthDate DATE CHECK (BirthDate <= GETDATE()) -- дата рождени€ не может быть в будущем
);
GO

-- “аблица оказанных услуг (основные услуги клиентам)
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),  -- уникальный идентификатор записи оказанной услуги
	AdditionalServiceID INT NOT NULL FOREIGN KEY REFERENCES AdditionalServices(AdditionalServiceID) ON DELETE CASCADE ON UPDATE CASCADE,  -- дополнительна€ услуга
    ClientID INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE,  -- клиент, которому оказана услуга
    EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID) ON DELETE CASCADE ON UPDATE CASCADE,  -- сотрудник, оказавший услугу
    Date DATE NOT NULL,  -- дата оказани€ услуги
    Cost DECIMAL(10, 2) NOT NULL CHECK (Cost >= 0)  -- стоимость услуги
);
GO