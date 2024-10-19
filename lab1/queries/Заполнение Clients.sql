-- �������� ��������� ������ ��� ����, �������, ������� � ���������
CREATE TABLE #MaleNames (Name NVARCHAR(50));
CREATE TABLE #FemaleNames (Name NVARCHAR(50));
CREATE TABLE #LastNames (LastName NVARCHAR(50));
CREATE TABLE #FemaleLastNames (FemaleLastName NVARCHAR(50));
CREATE TABLE #MalePatronymics (Patronymic NVARCHAR(50));
CREATE TABLE #FemalePatronymics (Patronymic NVARCHAR(50));
CREATE TABLE #Professions (Profession NVARCHAR(100));

-- ���������� ��������� ������
INSERT INTO #MaleNames (Name) VALUES
('���������'), ('������'), ('�������'), ('����'), ('��������'), 
('�������'), ('�����'), ('�����'), ('������'), ('�����');

INSERT INTO #FemaleNames (Name) VALUES
('����'), ('���������'), ('�����'), ('�����'), ('��������'), 
('�������'), ('�����'), ('�����'), ('�������'), ('����');

INSERT INTO #LastNames (LastName) VALUES
('������'), ('������'), ('�������'), ('��������'), ('������'), 
('Ը�����'), ('�������'), ('�����'), ('��������'), ('������');

INSERT INTO #FemaleLastNames (FemaleLastName) VALUES
('�������'), ('�������'), ('��������'), ('���������'), ('�������'), 
('Ը������'), ('��������'), ('������'), ('���������'), ('�������');

INSERT INTO #MalePatronymics (Patronymic) VALUES
('�������������'), ('����������'), ('����������'), ('��������'), 
('�����������'), ('����������'), ('��������'), ('��������'), 
('���������'), ('���������');

INSERT INTO #FemalePatronymics (Patronymic) VALUES
('�������������'), ('����������'), ('����������'), ('��������'), 
('�����������'), ('����������'), ('��������'), ('��������'), 
('���������'), ('���������');

INSERT INTO #Professions (Profession) VALUES
('�����������'), ('����'), ('�������'), ('�������'), ('����������'), 
('��������'), ('�������'), ('���������'), ('��������'), ('���������'),
('����������'), ('���������'), ('�����������'), ('��������'), ('�������'),
('��������'), ('�����'), ('���-�����'), ('��������'), ('�������');

-- ���������� ������� ������� � �������������� �������, ��������� � �����������
DECLARE @i INT = 1;
DECLARE @MaleName NVARCHAR(50);
DECLARE @FemaleName NVARCHAR(50);
DECLARE @LastName NVARCHAR(50);
DECLARE @FemaleLastName NVARCHAR(50);
DECLARE @MalePatronymic NVARCHAR(50);
DECLARE @FemalePatronymic NVARCHAR(50);
DECLARE @Profession NVARCHAR(100);

WHILE @i <= 500
BEGIN
    -- ��������� ���������
    SELECT TOP 1 @Profession = Profession FROM #Professions ORDER BY NEWID();

    IF @i % 2 = 0
    BEGIN
        -- ������� ������
        SELECT TOP 1 @FemaleName = Name FROM #FemaleNames ORDER BY NEWID();
        SELECT TOP 1 @FemaleLastName = FemaleLastName FROM #FemaleLastNames ORDER BY NEWID();
        SELECT TOP 1 @FemalePatronymic = Patronymic FROM #FemalePatronymics ORDER BY NEWID();

        INSERT INTO Clients (FirstName, LastName, MiddleName, Gender, BirthDate, ZodiacSignID, NationalityID, Profession)
        VALUES (
            @FemaleName, 
            @FemaleLastName, 
            @FemalePatronymic, 
            '�������', 
            DATEADD(YEAR, -FLOOR(RAND() * 50) - 18, GETDATE()),  -- ������� �� 18 �� 67 ���
            FLOOR(RAND() * 12) + 1,  -- ��������� ���� �������
            FLOOR(RAND() * 10) + 1,  -- ��������� ��������������
            @Profession  -- ��������� ���������
        );
    END
    ELSE
    BEGIN
        -- ������� ������
        SELECT TOP 1 @MaleName = Name FROM #MaleNames ORDER BY NEWID();
        SELECT TOP 1 @LastName = LastName FROM #LastNames ORDER BY NEWID();
        SELECT TOP 1 @MalePatronymic = Patronymic FROM #MalePatronymics ORDER BY NEWID();

        INSERT INTO Clients (FirstName, LastName, MiddleName, Gender, BirthDate, ZodiacSignID, NationalityID, Profession)
        VALUES (
            @MaleName, 
            @LastName, 
            @MalePatronymic, 
            '�������', 
            DATEADD(YEAR, -FLOOR(RAND() * 50) - 18, GETDATE()),  -- ������� �� 18 �� 67 ���
            FLOOR(RAND() * 12) + 1,  -- ��������� ���� �������
            FLOOR(RAND() * 10) + 1,  -- ��������� ��������������
            @Profession  -- ��������� ���������
        );
    END;

    SET @i = @i + 1;
END;

-- �������� ��������� ������
DROP TABLE #Professions;
DROP TABLE #MaleNames;
DROP TABLE #FemaleNames;
DROP TABLE #LastNames;
DROP TABLE #FemaleLastNames;
DROP TABLE #MalePatronymics;
DROP TABLE #FemalePatronymics;
