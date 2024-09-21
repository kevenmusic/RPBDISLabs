-- Создание временных таблиц для значений
CREATE TABLE #MaritalStatuses (Status NVARCHAR(50));
CREATE TABLE #BadHabits (Habit NVARCHAR(100));
CREATE TABLE #Hobbies (Hobby NVARCHAR(100));

-- Заполнение временных таблиц
INSERT INTO #MaritalStatuses (Status) VALUES
('Одинокий'), ('В браке'), ('Разведён'), ('Вдовец');

INSERT INTO #BadHabits (Habit) VALUES
('Курение'), ('Алкоголь'), ('Нет вредных привычек'), ('Игромания');

INSERT INTO #Hobbies (Hobby) VALUES
('Чтение'), ('Спорт'), ('Путешествия'), ('Музыка'), ('Рисование'), 
('Кулинария'), ('Танцы'), ('Фотография');

-- Переменные для хранения значений
DECLARE @Age INT;
DECLARE @Height DECIMAL(5, 2);
DECLARE @Weight DECIMAL(5, 2);
DECLARE @ChildrenCount INT;
DECLARE @MaritalStatus NVARCHAR(50);
DECLARE @BadHabits NVARCHAR(100);
DECLARE @Hobbies NVARCHAR(100);

-- Цикл для заполнения таблицы PhysicalAttributes
DECLARE @ClientID INT = 1;

WHILE @ClientID <= 500
BEGIN
    -- Случайные физические параметры
    SET @Age = FLOOR(RAND() * (67 - 18 + 1)) + 18;  -- Возраст от 18 до 67 лет
    SET @Height = FLOOR(RAND() * (200 - 150 + 1)) + 150;  -- Рост от 150 до 200 см
    SET @Weight = FLOOR(RAND() * (100 - 50 + 1)) + 50;    -- Вес от 50 до 100 кг
    SET @ChildrenCount = FLOOR(RAND() * 5);  -- Количество детей от 0 до 4

    -- Случайные значения для семейного положения, вредных привычек и хобби
    SELECT TOP 1 @MaritalStatus = Status FROM #MaritalStatuses ORDER BY NEWID();
    SELECT TOP 1 @BadHabits = Habit FROM #BadHabits ORDER BY NEWID();
    SELECT TOP 1 @Hobbies = Hobby FROM #Hobbies ORDER BY NEWID();

    -- Вставка данных в таблицу PhysicalAttributes
    INSERT INTO PhysicalAttributes (ClientID, Age, Height, Weight, ChildrenCount, MaritalStatus, BadHabits, Hobbies)
    VALUES (@ClientID, @Age, @Height, @Weight, @ChildrenCount, @MaritalStatus, @BadHabits, @Hobbies);

    -- Контрольный вывод для отладки
    PRINT 'Inserted physical attributes for client ' + CAST(@ClientID AS NVARCHAR(10));

    SET @ClientID = @ClientID + 1;
END;

-- Удаление временных таблиц
DROP TABLE #MaritalStatuses;
DROP TABLE #BadHabits;
DROP TABLE #Hobbies;
