CREATE VIEW ClientFullInfo AS
SELECT
    c.ClientID,
    c.FirstName,
    c.LastName,
    c.MiddleName,
    c.Gender,
    c.BirthDate,
    z.Name AS ZodiacSign,
    n.Name AS Nationality,
    c.Profession,
    p.Age,
    p.Height,
    p.Weight,
    p.ChildrenCount,
    p.MaritalStatus,
    p.BadHabits,
    p.Hobbies,
    con.Address,
    con.Phone,
    con.PassportData,
    ph.Photo
FROM
    Clients c
    LEFT JOIN PhysicalAttributes p ON c.ClientID = p.ClientID
    LEFT JOIN Contacts con ON c.ClientID = con.ClientID
    LEFT JOIN ZodiacSigns z ON c.ZodiacSignID = z.ZodiacSignID
    LEFT JOIN Nationalities n ON c.NationalityID = n.NationalityID
    LEFT JOIN Photos ph ON c.ClientID = ph.ClientID;