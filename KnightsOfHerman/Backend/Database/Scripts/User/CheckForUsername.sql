DROP PROCEDURE IF EXISTS CheckForUsername;
GO
CREATE PROCEDURE CheckForUsername
    @Username NVARCHAR(MAX)
AS 
BEGIN
    SELECT 
        CAST(
        CASE 
            WHEN EXISTS(Select 1 From KOH.Users WHERE UPPER(Username) = UPPER(@Username)) THEN 1
            ELSE 0
        END AS bit
        ) AS UsernameExists
END