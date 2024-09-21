CREATE VIEW ClientServices AS
SELECT
    c.ClientID,
    c.FirstName AS ClientFirstName,
    c.LastName AS ClientLastName,
    s.ServiceID,
    a.Name AS ServiceName,
    s.Date AS ServiceDate,
    s.Cost,
    e.FirstName AS EmployeeFirstName,
    e.LastName AS EmployeeLastName
FROM
    Clients c
    INNER JOIN Services s ON c.ClientID = s.ClientID
    INNER JOIN AdditionalServices a ON s.AdditionalServiceID = a.AdditionalServiceID
    INNER JOIN Employees e ON s.EmployeeID = e.EmployeeID;
