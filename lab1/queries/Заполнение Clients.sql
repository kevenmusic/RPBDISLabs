-- Создание временных таблиц для имен, фамилий, отчеств и профессий
CREATE TABLE #MaleNames (Name NVARCHAR(50));
CREATE TABLE #FemaleNames (Name NVARCHAR(50));
CREATE TABLE #LastNames (LastName NVARCHAR(50));
CREATE TABLE #FemaleLastNames (FemaleLastName NVARCHAR(50));
CREATE TABLE #MalePatronymics (Patronymic NVARCHAR(50));
CREATE TABLE #FemalePatronymics (Patronymic NVARCHAR(50));
CREATE TABLE #Professions (Profession NVARCHAR(100));

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

INSERT INTO #Professions (Profession) VALUES
('Программист'), ('Врач'), ('Учитель'), ('Инженер'), ('Маркетолог'), 
('Дизайнер'), ('Адвокат'), ('Экономист'), ('Менеджер'), ('Фармацевт'),
('Архитектор'), ('Журналист'), ('Полицейский'), ('Психолог'), ('Слесарь'),
('Электрик'), ('Повар'), ('Шеф-повар'), ('Водитель'), ('Флорист');

-- Заполнение таблицы Клиенты с разнообразными именами, фамилиями и профессиями
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
    -- Случайная профессия
    SELECT TOP 1 @Profession = Profession FROM #Professions ORDER BY NEWID();

    IF @i % 2 = 0
    BEGIN
        -- Женский клиент
        SELECT TOP 1 @FemaleName = Name FROM #FemaleNames ORDER BY NEWID();
        SELECT TOP 1 @FemaleLastName = FemaleLastName FROM #FemaleLastNames ORDER BY NEWID();
        SELECT TOP 1 @FemalePatronymic = Patronymic FROM #FemalePatronymics ORDER BY NEWID();

        INSERT INTO Clients (FirstName, LastName, MiddleName, Gender, BirthDate, ZodiacSignID, NationalityID, Profession)
        VALUES (
            @FemaleName, 
            @FemaleLastName, 
            @FemalePatronymic, 
            'Женский', 
            DATEADD(YEAR, -FLOOR(RAND() * 50) - 18, GETDATE()),  -- Возраст от 18 до 67 лет
            FLOOR(RAND() * 12) + 1,  -- Случайный знак зодиака
            FLOOR(RAND() * 10) + 1,  -- Случайная национальность
            @Profession  -- Случайная профессия
        );
    END
    ELSE
    BEGIN
        -- Мужской клиент
        SELECT TOP 1 @MaleName = Name FROM #MaleNames ORDER BY NEWID();
        SELECT TOP 1 @LastName = LastName FROM #LastNames ORDER BY NEWID();
        SELECT TOP 1 @MalePatronymic = Patronymic FROM #MalePatronymics ORDER BY NEWID();

        INSERT INTO Clients (FirstName, LastName, MiddleName, Gender, BirthDate, ZodiacSignID, NationalityID, Profession)
        VALUES (
            @MaleName, 
            @LastName, 
            @MalePatronymic, 
            'Мужской', 
            DATEADD(YEAR, -FLOOR(RAND() * 50) - 18, GETDATE()),  -- Возраст от 18 до 67 лет
            FLOOR(RAND() * 12) + 1,  -- Случайный знак зодиака
            FLOOR(RAND() * 10) + 1,  -- Случайная национальность
            @Profession  -- Случайная профессия
        );
    END;

    SET @i = @i + 1;
END;

-- Удаление временных таблиц
DROP TABLE #Professions;
DROP TABLE #MaleNames;
DROP TABLE #FemaleNames;
DROP TABLE #LastNames;
DROP TABLE #FemaleLastNames;
DROP TABLE #MalePatronymics;
DROP TABLE #FemalePatronymics;
