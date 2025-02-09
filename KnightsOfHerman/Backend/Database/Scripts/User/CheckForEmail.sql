DROP PROCEDURE IF EXISTS CheckForEmail;
GO
CREATE PROCEDURE CheckForEmail
    @Email NVARCHAR(MAX)
AS 
BEGIN
    SELECT 
        CAST(
        CASE 
            WHEN EXISTS(Select 1 From KOH.Users WHERE UPPER(Email) = UPPER(@Email)) THEN 1
            ELSE 0
        END AS bit
        ) AS EmailExists
END