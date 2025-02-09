DROP PROCEDURE IF EXISTS CreateCharacterAbility
GO
CREATE PROCEDURE CreateCharacterAbility
    @CharacterID INT,
    @AbilityType INT
AS
BEGIN
    INSERT INTO Sanctum.CharacterAbilities (CharacterID, AbilityType)
    OUTPUT INSERTED.AbilityID, INSERTED.Title, INSERTED.Content, INSERTED.Cost, INSERTED.Memorized, INSERTED.CharacterID, INSERTED.AbilityType
    VALUES (@CharacterID, @AbilityType);
END
GO
DROP PROCEDURE IF EXISTS GetCharacterAbilities
GO
CREATE PROCEDURE GetCharacterAbilities
    @CharacterID INT
AS
BEGIN
    SELECT AbilityID, Title, Content, Cost, Memorized, CharacterID, AbilityType
    FROM Sanctum.CharacterAbilities
    WHERE CharacterID = @CharacterID;
END
GO
DROP PROCEDURE IF EXISTS SaveCharacterAbility
GO
CREATE PROCEDURE SaveCharacterAbility
    @AbilityID INT,
    @Title NVARCHAR(255),
    @Content NVARCHAR(3000),
    @Cost NVARCHAR(255),
    @Memorized BIT
AS
BEGIN
    UPDATE Sanctum.CharacterAbilities
    SET Title = @Title,
        Content = @Content,
        Cost = @Cost,
        Memorized = @Memorized
    WHERE AbilityID = @AbilityID;
END
GO
DROP PROCEDURE IF EXISTS DeleteCharacterAbility
GO
CREATE PROCEDURE DeleteCharacterAbility
    @AbilityID INT
AS
BEGIN
    DELETE FROM Sanctum.CharacterAbilities
    WHERE AbilityID = @AbilityID;
END
GO