-- �������� ���� ������
CREATE DATABASE MarriageAgencyDB;
GO

-- ������������� ���� ������
USE MarriageAgencyDB;
GO

-- ������� ����� �������
CREATE TABLE ZodiacSigns (
    ZodiacSignId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- ���������� �������� ������
    Description NVARCHAR(MAX) DEFAULT '��� ��������' -- �������� �� ���������
);
GO

-- ������� ��������������
CREATE TABLE Nationalities (
    NationalityId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- ���������� �������� ���������������
    Notes NVARCHAR(MAX) DEFAULT '��� ��������� �������' -- �������� �� ���������
);
GO

-- ������� ������ ������ ��������
CREATE TABLE Clients (
    ClientId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('�������', '�������')), -- ��������� ������ ������� ��� �������
    BirthDate DATE CHECK (BirthDate <= GETDATE()), -- ���� �������� �� ����� ���� � �������
    ZodiacSignId INT FOREIGN KEY REFERENCES ZodiacSigns(ZodiacSignId) ON DELETE SET NULL ON UPDATE CASCADE,
    NationalityId INT FOREIGN KEY REFERENCES Nationalities(NationalityId) ON DELETE SET NULL ON UPDATE CASCADE,
    Profession NVARCHAR(100) DEFAULT '�����������', -- �������� �� ���������
    ClientPhoto NVARCHAR(MAX) -- ����������: ��������� �������
);
GO

-- ������� ��������� ��������
CREATE TABLE Contacts (
    ClientId INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientId) ON DELETE CASCADE ON UPDATE CASCADE,
    Address NVARCHAR(255),
    Phone NVARCHAR(20) UNIQUE, -- ���������� ����� ��������
    PassportData NVARCHAR(100) UNIQUE -- ���������� ���������� ������
);
GO

-- ������� ���������� ���������� � ��������
CREATE TABLE PhysicalAttributes (
    ClientId INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientId) ON DELETE CASCADE ON UPDATE CASCADE,
    Age INT CHECK (Age >= 18), -- ����������� ������� 18
    Height DECIMAL(5,2) CHECK (Height >= 0), -- ���� �� ����� ���� �������������
    Weight DECIMAL(5,2) CHECK (Weight >= 0), -- ��� �� ����� ���� �������������
    ChildrenCount INT CHECK (ChildrenCount >= 0), -- ���������� ����� �� ����� ���� �������������
    MaritalStatus NVARCHAR(50) DEFAULT '��������', -- �������� �� ���������
    BadHabits NVARCHAR(MAX),
    Hobbies NVARCHAR(MAX)
);
GO

-- ������� �������������� �����
CREATE TABLE AdditionalServices (
    AdditionalServiceId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE, -- ���������� �������� �����
    Description NVARCHAR(MAX) DEFAULT '��� ��������', -- �������� �� ���������
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0) -- ���� ������ ���� �������������
);
GO

-- ������� �����������
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(100) DEFAULT '�����������', -- �������� �� ���������
    BirthDate DATE CHECK (BirthDate <= GETDATE()) -- ���� �������� �� ����� ���� � �������
);
GO

-- ������� ��������� ����� (�������� ������ ��������)
CREATE TABLE Services (
    ServiceId INT PRIMARY KEY IDENTITY(1,1),  -- ���������� ������������� ������ ��������� ������
    AdditionalServiceId INT NOT NULL FOREIGN KEY REFERENCES AdditionalServices(AdditionalServiceId) ON DELETE CASCADE ON UPDATE CASCADE,  -- �������������� ������
    ClientId INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientId) ON DELETE CASCADE ON UPDATE CASCADE,  -- ������, �������� ������� ������
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId) ON DELETE CASCADE ON UPDATE CASCADE,  -- ���������, ��������� ������
    Date DATE NOT NULL,  -- ���� �������� ������
    Cost DECIMAL(10, 2) NOT NULL CHECK (Cost >= 0)  -- ��������� ������
);
GO
