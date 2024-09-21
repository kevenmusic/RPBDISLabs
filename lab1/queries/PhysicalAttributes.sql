-- �������� ��������� ������ ��� ��������
CREATE TABLE #MaritalStatuses (Status NVARCHAR(50));
CREATE TABLE #BadHabits (Habit NVARCHAR(100));
CREATE TABLE #Hobbies (Hobby NVARCHAR(100));

-- ���������� ��������� ������
INSERT INTO #MaritalStatuses (Status) VALUES
('��������'), ('� �����'), ('�������'), ('������');

INSERT INTO #BadHabits (Habit) VALUES
('�������'), ('��������'), ('��� ������� ��������'), ('���������');

INSERT INTO #Hobbies (Hobby) VALUES
('������'), ('�����'), ('�����������'), ('������'), ('���������'), 
('���������'), ('�����'), ('����������');

-- ���������� ��� �������� ��������
DECLARE @Age INT;
DECLARE @Height DECIMAL(5, 2);
DECLARE @Weight DECIMAL(5, 2);
DECLARE @ChildrenCount INT;
DECLARE @MaritalStatus NVARCHAR(50);
DECLARE @BadHabits NVARCHAR(100);
DECLARE @Hobbies NVARCHAR(100);

-- ���� ��� ���������� ������� PhysicalAttributes
DECLARE @ClientID INT = 1;

WHILE @ClientID <= 500
BEGIN
    -- ��������� ���������� ���������
    SET @Age = FLOOR(RAND() * (67 - 18 + 1)) + 18;  -- ������� �� 18 �� 67 ���
    SET @Height = FLOOR(RAND() * (200 - 150 + 1)) + 150;  -- ���� �� 150 �� 200 ��
    SET @Weight = FLOOR(RAND() * (100 - 50 + 1)) + 50;    -- ��� �� 50 �� 100 ��
    SET @ChildrenCount = FLOOR(RAND() * 5);  -- ���������� ����� �� 0 �� 4

    -- ��������� �������� ��� ��������� ���������, ������� �������� � �����
    SELECT TOP 1 @MaritalStatus = Status FROM #MaritalStatuses ORDER BY NEWID();
    SELECT TOP 1 @BadHabits = Habit FROM #BadHabits ORDER BY NEWID();
    SELECT TOP 1 @Hobbies = Hobby FROM #Hobbies ORDER BY NEWID();

    -- ������� ������ � ������� PhysicalAttributes
    INSERT INTO PhysicalAttributes (ClientID, Age, Height, Weight, ChildrenCount, MaritalStatus, BadHabits, Hobbies)
    VALUES (@ClientID, @Age, @Height, @Weight, @ChildrenCount, @MaritalStatus, @BadHabits, @Hobbies);

    -- ����������� ����� ��� �������
    PRINT 'Inserted physical attributes for client ' + CAST(@ClientID AS NVARCHAR(10));

    SET @ClientID = @ClientID + 1;
END;

-- �������� ��������� ������
DROP TABLE #MaritalStatuses;
DROP TABLE #BadHabits;
DROP TABLE #Hobbies;
