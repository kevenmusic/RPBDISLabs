CREATE PROCEDURE UpdatePhysicalAttributes
    @ClientID INT,
    @Age INT,
    @Height DECIMAL(5,2),
    @Weight DECIMAL(5,2),
    @ChildrenCount INT,
    @MaritalStatus NVARCHAR(50),
    @BadHabits NVARCHAR(MAX) = NULL,
    @Hobbies NVARCHAR(MAX) = NULL
AS
BEGIN
    UPDATE PhysicalAttributes
    SET 
        Age = @Age,
        Height = @Height,
        Weight = @Weight,
        ChildrenCount = @ChildrenCount,
        MaritalStatus = @MaritalStatus,
        BadHabits = @BadHabits,
        Hobbies = @Hobbies
    WHERE ClientID = @ClientID;
END;
