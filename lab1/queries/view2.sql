CREATE VIEW EmployeeServices AS
SELECT
    e.EmployeeID,
    e.FirstName AS EmployeeFirstName,
    e.LastName AS EmployeeLastName,
    e.Position,
    s.ServiceID,
    s.ClientID,
    c.FirstName AS ClientFirstName,
    c.LastName AS ClientLastName,
    s.Date AS ServiceDate,
    s.Cost,
    a.Name AS ServiceName
FROM
    Employees e
    INNER JOIN Services s ON e.EmployeeID = s.EmployeeID
    INNER JOIN Clients c ON s.ClientID = c.ClientID
    INNER JOIN AdditionalServices a ON s.AdditionalServiceID = a.AdditionalServiceID;
