CREATE TABLE KOH.Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255) UNIQUE,
    Email NVARCHAR(255) UNIQUE,
    Password NVARCHAR(255),
    Salt NVARCHAR(255)
);

