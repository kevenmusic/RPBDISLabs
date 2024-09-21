CREATE PROCEDURE UpsertAdditionalService
    @AdditionalServiceID INT = NULL,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX) = 'Нет описания',
    @Price DECIMAL(10, 2)
AS
BEGIN
    IF @AdditionalServiceID IS NULL
    BEGIN
        -- Вставка нового сервиса
        INSERT INTO AdditionalServices (Name, Description, Price)
        VALUES (@Name, @Description, @Price);
    END
    ELSE
    BEGIN
        -- Обновление существующего сервиса
        UPDATE AdditionalServices
        SET 
            Name = @Name,
            Description = @Description,
            Price = @Price
        WHERE AdditionalServiceID = @AdditionalServiceID;
    END
END;
