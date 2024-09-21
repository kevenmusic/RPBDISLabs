-- Заполнение таблицы Photos
DECLARE @i INT = 1;
DECLARE @PhotoPath NVARCHAR(MAX);

WHILE @i <= 500
BEGIN
    -- Генерация уникального пути к фотографии
    SET @PhotoPath = CONCAT('photos/client_', @i, '.jpg');

    -- Вставка в таблицу Photos
    INSERT INTO Photos (ClientID, Photo)
    VALUES (
        @i,  -- Связь с ClientID
        @PhotoPath  -- Путь к фото
    );

    SET @i = @i + 1;
END;
