-- Заполнение таблицы Services
DECLARE @i INT = 1;
DECLARE @ClientID INT;
DECLARE @EmployeeID INT;
DECLARE @AdditionalServiceID INT;
DECLARE @Date DATE;
DECLARE @Cost DECIMAL(10, 2);

WHILE @i <= 20000  -- Допустим, у нас 20 000 записей об оказанных услугах
BEGIN
    -- Случайный выбор существующего клиента, сотрудника и дополнительной услуги
    SET @ClientID = FLOOR(RAND() * 500) + 1;  -- Существующие клиенты (500 записей)
    SET @EmployeeID = FLOOR(RAND() * 100) + 1;  -- Существующие сотрудники (100 записей)
    SET @AdditionalServiceID = FLOOR(RAND() * 10) + 1;  -- Существующие дополнительные услуги (10 записей)

    -- Случайная дата оказания услуги за последние 2 года
    SET @Date = DATEADD(DAY, -FLOOR(RAND() * 730), GETDATE());

    -- Получение стоимости услуги из таблицы AdditionalServices
    SELECT TOP 1 @Cost = Price FROM AdditionalServices WHERE AdditionalServiceID = @AdditionalServiceID;

    -- Вставка в таблицу Services
    INSERT INTO Services (AdditionalServiceID, ClientID, EmployeeID, Date, Cost)
    VALUES (
        @AdditionalServiceID,  -- Случайная услуга
        @ClientID,  -- Случайный клиент
        @EmployeeID,  -- Случайный сотрудник
        @Date,  -- Случайная дата оказания услуги
        @Cost  -- Стоимость услуги
    );

    SET @i = @i + 1;
END;
