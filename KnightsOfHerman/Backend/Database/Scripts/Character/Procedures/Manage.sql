DROP PROCEDURE IF EXISTS CreateBlankCharacter;
GO
CREATE PROCEDURE CreateBlankCharacter
    @UserID INT
AS
BEGIN
    INSERT INTO Sanctum.Characters (OwnerID)
    VALUES (@UserID);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Value
END
GO
DROP PROCEDURE IF EXISTS DeleteCharacter;
GO
CREATE PROCEDURE DeleteCharacter
    @UserID INT,
    @CharacterID INT
AS
BEGIN
    DELETE FROM Sanctum.Characters
    WHERE CharacterID = @CharacterID AND OwnerID = @UserID;
END
GO
DROP PROCEDURE IF EXISTS GetCharacterCount;
GO
CREATE PROCEDURE GetCharacterCount
    @UserID INT
AS
BEGIN
    SELECT COUNT(*) 
    FROM Sanctum.Characters 
    WHERE OwnerID = @UserID
END
GO 
DROP PROCEDURE IF EXISTS GetCharacterPermissions;
GO
CREATE PROCEDURE GetCharacterPermissions
    @CharacterID INT
AS
BEGIN
    -- Get Owner ID
    SELECT 
        c.OwnerID as UserID, 
        1 as AccessLevel --Ownership access level
    FROM 
        Sanctum.Characters AS c
    WHERE 
        c.CharacterID = @CharacterID
    UNION ALL
    
    SELECT 
        s.UserID,
        s.AccessLevel
    FROM 
        Sanctum.SharedCharacters as s
    WHERE 
        s.CharacterID = @CharacterID
END
