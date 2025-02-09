DROP PROCEDURE IF EXISTS GetCharacterShareInfo;
GO
CREATE PROCEDURE GetCharacterShareInfo
    @OwnerID INT,
    @CharacterID INT
AS
BEGIN
    SELECT 
        u.Username,
        sc.AccessLevel
    FROM Sanctum.SharedCharacters sc
        LEFT JOIN KOH.Users u ON sc.UserID = u.UserID
        LEFT JOIN Sanctum.Characters c ON c.CharacterID = @CharacterID 
    WHERE sc.CharacterID = @CharacterID  AND c.OwnerID = @OwnerID
END