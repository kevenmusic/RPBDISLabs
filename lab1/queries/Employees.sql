-- Создание временных таблиц для имен, фамилий, отчеств и позиций
CREATE TABLE #MaleNames (Name NVARCHAR(50));
CREATE TABLE #FemaleNames (Name NVARCHAR(50));
CREATE TABLE #LastNames (LastName NVARCHAR(50));
CREATE TABLE #FemaleLastNames (FemaleLastName NVARCHAR(50));
CREATE TABLE #MalePatronymics (Patronymic NVARCHAR(50));
CREATE TABLE #FemalePatronymics (Patronymic NVARCHAR(50));
CREATE TABLE #Positions (Position NVARCHAR(100));

-- Заполнение временных таблиц
INSERT INTO #MaleNames (Name) VALUES
('Александр'), ('Максим'), ('Дмитрий'), ('Иван'), ('Анатолий'), 
('Евгений'), ('Павел'), ('Артем'), ('Сергей'), ('Роман');

INSERT INTO #FemaleNames (Name) VALUES
('Анна'), ('Екатерина'), ('Мария'), ('Ольга'), ('Светлана'), 
('Наталья'), ('Дарья'), ('Елена'), ('Татьяна'), ('Юлия');

INSERT INTO #LastNames (LastName) VALUES
('Иванов'), ('Петров'), ('Сидоров'), ('Кузнецов'), ('Семёнов'), 
('Фёдоров'), ('Морозов'), ('Попов'), ('Васильев'), ('Зайцев');

INSERT INTO #FemaleLastNames (FemaleLastName) VALUES
('Иванова'), ('Петрова'), ('Сидорова'), ('Кузнецова'), ('Семёнова'), 
('Фёдорова'), ('Морозова'), ('Попова'), ('Васильева'), ('Зайцева');

INSERT INTO #MalePatronymics (Patronymic) VALUES
('Александрович'), ('Максимович'), ('Дмитриевич'), ('Иванович'), 
('Анатольевич'), ('Евгеньевич'), ('Павлович'), ('Артёмович'), 
('Сергеевич'), ('Романович');

INSERT INTO #FemalePatronymics (Patronymic) VALUES
('Александровна'), ('Максимовна'), ('Дмитриевна'), ('Ивановна'), 
('Анатольевна'), ('Евгеньевна'), ('Павловна'), ('Артёмовна'), 
('Сергеевна'), ('Романовна');

-- Добавление позиций сотрудников
INSERT INTO #Positions (Position) VALUES
('Менеджер по работе с клиентами'), 
('Консультант'), 
('HR специалист'), 
('Маркетолог'), 
('Аналитик'), 
('IT специалист'), 
('Бухгалтер'), 
('Юрист'), 
('Офис-менеджер'), 
('Специалист по рекламе');

-- Заполнение таблицы Employees с разнообразными именами, фамилиями и позициями
DECLARE @i INT = 1;
DECLARE @MaleName NVARCHAR(50);
DECLARE @FemaleName NVARCHAR(50);
DECLARE @LastName NVARCHAR(50);
DECLARE @FemaleLastName NVARCHAR(50);
DECLARE @MalePatronymic NVARCHAR(50);
DECLARE @FemalePatronymic NVARCHAR(50);
DECLARE @Position NVARCHAR(100);
WHILE @i <= 500  -- 500 сотрудников
BEGIN
    -- Случайный выбор позиции
    SELECT TOP 1 @Position = Position FROM #Positions ORDER BY NEWID();

    IF @i % 2 = 0
    BEGIN
        -- Женский сотрудник
        SELECT TOP 1 @FemaleName = Name FROM #FemaleNames ORDER BY NEWID();
        SELECT TOP 1 @FemaleLastName = FemaleLastName FROM #FemaleLastNames ORDER BY NEWID();
        SELECT TOP 1 @FemalePatronymic = Patronymic FROM #FemalePatronymics ORDER BY NEWID();

        INSERT INTO Employees (FirstName, LastName, MiddleName, BirthDate, Position)
        VALUES (
            @FemaleName, 
            @FemaleLastName, 
            @FemalePatronymic, 
            DATEADD(YEAR, -FLOOR(RAND() * 40) - 18, GETDATE()),  -- Возраст от 18 до 58 лет
            @Position  -- Случайная должность
        );
    END
    ELSE
    BEGIN
        -- Мужской сотрудник
        SELECT TOP 1 @MaleName = Name FROM #MaleNames ORDER BY NEWID();
        SELECT TOP 1 @LastName = LastName FROM #LastNames ORDER BY NEWID();
        SELECT TOP 1 @MalePatronymic = Patronymic FROM #MalePatronymics ORDER BY NEWID();

        INSERT INTO Employees (FirstName, LastName, MiddleName, BirthDate, Position)
        VALUES (
            @MaleName, 
            @LastName, 
            @MalePatronymic, 
            DATEADD(YEAR, -FLOOR(RAND() * 40) - 18, GETDATE()),  -- Возраст от 18 до 58 лет
            @Position  -- Случайная должность
        );
    END;
    SET @i = @i + 1;
END;

-- Удаление временных таблиц
DROP TABLE #MaleNames;
DROP TABLE #FemaleNames;
DROP TABLE #LastNames;
DROP TABLE #FemaleLastNames;
DROP TABLE #MalePatronymics;
DROP TABLE #FemalePatronymics;
DROP TABLE #Positions;
