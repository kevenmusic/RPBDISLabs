-- �������� ���� ������
CREATE DATABASE MarriageAgency;
GO

-- ������������� ���� ������
USE MarriageAgency;
GO

-- ������� ����� �������
CREATE TABLE ZodiacSigns (
    ZodiacSignID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- ���������� �������� ������
    Description NVARCHAR(MAX) DEFAULT '��� ��������' -- �������� �� ���������
);
GO

-- ������� ��������������
CREATE TABLE Nationalities (
    NationalityID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE, -- ���������� �������� ���������������
    Notes NVARCHAR(MAX) DEFAULT '��� ��������� �������' -- �������� �� ���������
);
GO

-- ������� ������ ������ ��������
CREATE TABLE Clients (
    ClientID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    Gender NVARCHAR(10) NOT NULL CHECK (Gender IN ('�������', '�������')), -- ��������� ������ ������� ��� �������
    BirthDate DATE CHECK (BirthDate <= GETDATE()), -- ���� �������� �� ����� ���� � �������
    ZodiacSignID INT FOREIGN KEY REFERENCES ZodiacSigns(ZodiacSignID) ON DELETE SET NULL ON UPDATE CASCADE,
    NationalityID INT FOREIGN KEY REFERENCES Nationalities(NationalityID) ON DELETE SET NULL ON UPDATE CASCADE,
    Profession NVARCHAR(100) DEFAULT '�����������' -- �������� �� ���������
);
GO

-- ������� ��������� ��������
CREATE TABLE Contacts (
    ClientID INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE,
    Address NVARCHAR(255),
    Phone NVARCHAR(20) UNIQUE, -- ���������� ����� ��������
    PassportData NVARCHAR(100) UNIQUE -- ���������� ���������� ������
);
GO

-- ������� ���������� ���������� � ��������
CREATE TABLE PhysicalAttributes (
    ClientID INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE,
    Age INT CHECK (Age >= 18), -- ����������� ������� 18
    Height DECIMAL(5,2) CHECK (Height >= 0), -- ���� �� ����� ���� �������������
    Weight DECIMAL(5,2) CHECK (Weight >= 0), -- ��� �� ����� ���� �������������
    ChildrenCount INT CHECK (ChildrenCount >= 0), -- ���������� ����� �� ����� ���� �������������
    MaritalStatus NVARCHAR(50) DEFAULT '��������', -- �������� �� ���������
    BadHabits NVARCHAR(MAX),
    Hobbies NVARCHAR(MAX)
);
GO

-- ������� ���������� �������� (����-�-������ � �������� ��������)
CREATE TABLE Photos (
    ClientID INT PRIMARY KEY FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE, -- ����� ���� � ������
    Photo NVARCHAR(MAX) NOT NULL -- ���� � ���� ��� �������� ������
);
GO

-- ������� �������������� �����
CREATE TABLE AdditionalServices (
    AdditionalServiceID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE, -- ���������� �������� �����
    Description NVARCHAR(MAX) DEFAULT '��� ��������', -- �������� �� ���������
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0) -- ���� ������ ���� �������������
);
GO

-- ������� �����������
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(100) DEFAULT '�����������', -- �������� �� ���������
    BirthDate DATE CHECK (BirthDate <= GETDATE()) -- ���� �������� �� ����� ���� � �������
);
GO

-- ������� ��������� ����� (�������� ������ ��������)
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),  -- ���������� ������������� ������ ��������� ������
	AdditionalServiceID INT NOT NULL FOREIGN KEY REFERENCES AdditionalServices(AdditionalServiceID) ON DELETE CASCADE ON UPDATE CASCADE,  -- �������������� ������
    ClientID INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientID) ON DELETE CASCADE ON UPDATE CASCADE,  -- ������, �������� ������� ������
    EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID) ON DELETE CASCADE ON UPDATE CASCADE,  -- ���������, ��������� ������
    Date DATE NOT NULL,  -- ���� �������� ������
    Cost DECIMAL(10, 2) NOT NULL CHECK (Cost >= 0)  -- ��������� ������
);
GO