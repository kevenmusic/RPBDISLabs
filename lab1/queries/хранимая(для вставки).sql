CREATE PROCEDURE InsertClient
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @MiddleName NVARCHAR(50) = NULL,
    @Gender NVARCHAR(10),
    @BirthDate DATE,
    @ZodiacSignID INT = NULL,
    @NationalityID INT = NULL,
    @Profession NVARCHAR(100) = 'Безработный'
AS
BEGIN
    INSERT INTO Clients (FirstName, LastName, MiddleName, Gender, BirthDate, ZodiacSignID, NationalityID, Profession)
    VALUES (@FirstName, @LastName, @MiddleName, @Gender, @BirthDate, @ZodiacSignID, @NationalityID, @Profession);
END;
