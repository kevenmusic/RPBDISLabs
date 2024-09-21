-- ���������� ������� Photos
DECLARE @i INT = 1;
DECLARE @PhotoPath NVARCHAR(MAX);

WHILE @i <= 500
BEGIN
    -- ��������� ����������� ���� � ����������
    SET @PhotoPath = CONCAT('photos/client_', @i, '.jpg');

    -- ������� � ������� Photos
    INSERT INTO Photos (ClientID, Photo)
    VALUES (
        @i,  -- ����� � ClientID
        @PhotoPath  -- ���� � ����
    );

    SET @i = @i + 1;
END;
