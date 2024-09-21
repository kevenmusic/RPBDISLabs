-- Заполнение таблицы Contacts
DECLARE @i INT = 1;
DECLARE @Phone NVARCHAR(20);
DECLARE @PassportData NVARCHAR(100);

WHILE @i <= 500
BEGIN
    -- Генерация уникального телефонного номера и паспортных данных
    SET @Phone = CONCAT('+7', FLOOR(RAND() * 9000000000 + 1000000000));
    SET @PassportData = CONCAT(FLOOR(RAND() * 899999 + 100000), ' ', FLOOR(RAND() * 899999 + 100000));

    -- Вставка в таблицу Contacts
    INSERT INTO Contacts (ClientID, Address, Phone, PassportData)
    VALUES (
        @i,  -- Связь с ClientID
        CONCAT('Улица ', @i, ', дом ', FLOOR(RAND() * 100 + 1), ', квартира ', FLOOR(RAND() * 100 + 1)),  -- Адрес
        @Phone,  -- Телефон
        @PassportData  -- Паспортные данные
    );

    SET @i = @i + 1;
END;