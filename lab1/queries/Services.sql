-- ���������� ������� Services
DECLARE @i INT = 1;
DECLARE @ClientID INT;
DECLARE @EmployeeID INT;
DECLARE @AdditionalServiceID INT;
DECLARE @Date DATE;
DECLARE @Cost DECIMAL(10, 2);

WHILE @i <= 20000  -- ��������, � ��� 20 000 ������� �� ��������� �������
BEGIN
    -- ��������� ����� ������������� �������, ���������� � �������������� ������
    SET @ClientID = FLOOR(RAND() * 500) + 1;  -- ������������ ������� (500 �������)
    SET @EmployeeID = FLOOR(RAND() * 100) + 1;  -- ������������ ���������� (100 �������)
    SET @AdditionalServiceID = FLOOR(RAND() * 10) + 1;  -- ������������ �������������� ������ (10 �������)

    -- ��������� ���� �������� ������ �� ��������� 2 ����
    SET @Date = DATEADD(DAY, -FLOOR(RAND() * 730), GETDATE());

    -- ��������� ��������� ������ �� ������� AdditionalServices
    SELECT TOP 1 @Cost = Price FROM AdditionalServices WHERE AdditionalServiceID = @AdditionalServiceID;

    -- ������� � ������� Services
    INSERT INTO Services (AdditionalServiceID, ClientID, EmployeeID, Date, Cost)
    VALUES (
        @AdditionalServiceID,  -- ��������� ������
        @ClientID,  -- ��������� ������
        @EmployeeID,  -- ��������� ���������
        @Date,  -- ��������� ���� �������� ������
        @Cost  -- ��������� ������
    );

    SET @i = @i + 1;
END;
