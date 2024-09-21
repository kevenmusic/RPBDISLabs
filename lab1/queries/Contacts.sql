-- ���������� ������� Contacts
DECLARE @i INT = 1;
DECLARE @Phone NVARCHAR(20);
DECLARE @PassportData NVARCHAR(100);

WHILE @i <= 500
BEGIN
    -- ��������� ����������� ����������� ������ � ���������� ������
    SET @Phone = CONCAT('+7', FLOOR(RAND() * 9000000000 + 1000000000));
    SET @PassportData = CONCAT(FLOOR(RAND() * 899999 + 100000), ' ', FLOOR(RAND() * 899999 + 100000));

    -- ������� � ������� Contacts
    INSERT INTO Contacts (ClientID, Address, Phone, PassportData)
    VALUES (
        @i,  -- ����� � ClientID
        CONCAT('����� ', @i, ', ��� ', FLOOR(RAND() * 100 + 1), ', �������� ', FLOOR(RAND() * 100 + 1)),  -- �����
        @Phone,  -- �������
        @PassportData  -- ���������� ������
    );

    SET @i = @i + 1;
END;