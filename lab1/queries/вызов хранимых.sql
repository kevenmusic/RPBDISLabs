-- Вставка нового клиента
EXEC InsertClient
    @FirstName = 'Олег',
    @LastName = 'Иванов',
    @MiddleName = 'Олегович',
    @Gender = 'Мужской',
    @BirthDate = '1990-03-20',
    @ZodiacSignID = 5, -- Лев
    @NationalityID = 6, -- Итальянец
    @Profession = 'Бухгалтер';

	-- Обновление физических характеристик клиента
EXEC UpdatePhysicalAttributes
    @ClientID = 1,
    @Age = 39,
    @Height = 181.00,
    @Weight = 76.00,
    @ChildrenCount = 2,
    @MaritalStatus = 'Женат',
    @BadHabits = 'Курение',
    @Hobbies = 'Спорт, чтение';

	-- Вставка нового сервиса
EXEC UpsertAdditionalService
    @Name = 'Астрологическая консультация',
    @Description = 'Консультация по вопросам астрологии',
    @Price = 4500.00;

-- Обновление существующего сервиса
EXEC UpsertAdditionalService
    @AdditionalServiceID = 1, -- Сервис с ID = 1
    @Name = 'Изменённое название сервиса',
    @Description = 'Обновлённое описание сервиса',
    @Price = 5500.00;
