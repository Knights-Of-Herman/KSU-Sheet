DROP PROCEDURE IF EXISTS ShareCharacter;
GO
CREATE PROCEDURE ShareCharacter
    @OwnerID INT,
    @ShareUsername NVARCHAR(MAX),
    @CharacterID INT,
    @AccessLevel INT
AS
BEGIN
    BEGIN TRY
        DECLARE @ShareUserID INT;

        SELECT @ShareUserID = UserID FROM KOH.Users WHERE Username = @ShareUsername;
        IF @ShareUserID IS NULL
        BEGIN
            SELECT 1 AS ReturnValue; -- User not found
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM Sanctum.Characters WHERE CharacterID = @CharacterID)
        BEGIN
            SELECT 2 AS ReturnValue; -- Character not found
            RETURN;
        END
        
        IF NOT EXISTS (SELECT 1 FROM Sanctum.Characters WHERE OwnerID = @OwnerID AND CharacterID = @CharacterID)
        BEGIN
            SELECT 3 AS ReturnValue; -- Not authorized
            RETURN;
        END

        -- When AccessLevel is 0, delete the entry if it exists
        IF @AccessLevel = 0
        BEGIN
            IF EXISTS (
                SELECT 1
                FROM Sanctum.SharedCharacters
                WHERE CharacterID = @CharacterID AND UserID = @ShareUserID
            )
            BEGIN
                DELETE FROM Sanctum.SharedCharacters
                WHERE CharacterID = @CharacterID AND UserID = @ShareUserID;
            END
            SELECT 0 AS ReturnValue; -- Success, entry deleted or no action needed
            RETURN;
        END

        -- Perform the update
        IF EXISTS (SELECT 1 FROM Sanctum.SharedCharacters WHERE CharacterID = @CharacterID AND UserID = @ShareUserID)
        BEGIN
            UPDATE Sanctum.SharedCharacters
            SET AccessLevel = @AccessLevel
            WHERE CharacterID = @CharacterID AND UserID = @ShareUserID;
        END
        ELSE
        BEGIN
            INSERT INTO Sanctum.SharedCharacters (CharacterID, UserID, AccessLevel)
            VALUES (@CharacterID, @ShareUserID, @AccessLevel);
        END
        
        SELECT 0 AS ReturnValue; -- Success
    END TRY
    BEGIN CATCH
        SELECT 4 AS ReturnValue; -- Unknown error
    END CATCH
END;

