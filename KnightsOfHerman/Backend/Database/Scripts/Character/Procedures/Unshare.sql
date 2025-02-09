DROP PROCEDURE IF EXISTS UnShareCharacter;
GO
CREATE PROCEDURE UnShareCharacter
    @UserID INT,
    @CharacterID INT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Sanctum.SharedCharacters
        WHERE CharacterID = @CharacterID AND UserID = @UserID
    )
    BEGIN
        DELETE FROM Sanctum.SharedCharacters
        WHERE CharacterID = @CharacterID AND UserID = @UserID
    END
END