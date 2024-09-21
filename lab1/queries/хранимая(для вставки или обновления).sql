CREATE PROCEDURE UpsertAdditionalService
    @AdditionalServiceID INT = NULL,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX) = '��� ��������',
    @Price DECIMAL(10, 2)
AS
BEGIN
    IF @AdditionalServiceID IS NULL
    BEGIN
        -- ������� ������ �������
        INSERT INTO AdditionalServices (Name, Description, Price)
        VALUES (@Name, @Description, @Price);
    END
    ELSE
    BEGIN
        -- ���������� ������������� �������
        UPDATE AdditionalServices
        SET 
            Name = @Name,
            Description = @Description,
            Price = @Price
        WHERE AdditionalServiceID = @AdditionalServiceID;
    END
END;
