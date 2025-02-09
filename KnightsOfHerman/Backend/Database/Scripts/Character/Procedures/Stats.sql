-- Call this only on Character Creation
DROP PROCEDURE IF EXISTS CreateCharacterStats
GO
CREATE PROCEDURE CreateCharacterStats
    @CharacterID INT,
    @StatIDs NVARCHAR(MAX)
AS
BEGIN
    -- Split the @StatIDs string and insert each stat into Sanctum.CharacterStats
    INSERT INTO Sanctum.CharacterStats (CharacterID, StatID)
    SELECT @CharacterID, value
    FROM STRING_SPLIT(@StatIDs, ',') AS Stats
    WHERE TRY_CONVERT(INT, Stats.value) IS NOT NULL;
END
GO
DROP PROCEDURE IF EXISTS GetCharacterStats
GO
CREATE PROCEDURE GetCharacterStats
    @CharacterID INT
AS
BEGIN
    SELECT 
        StatID,
        Base,
        CustomMod,
        OverrideValue,
        DoOverride
    FROM Sanctum.CharacterStats
    WHERE CharacterID = @CharacterID
END
GO
DROP PROCEDURE IF EXISTS SaveCharacterStat
GO
CREATE PROCEDURE SaveCharacterStat
    @CharacterID INT,
    @StatID INT,
    @Base SMALLINT,
    @CustomMod SMALLINT,
    @OverrideValue SMALLINT,
    @DoOverride BIT
AS
BEGIN
    MERGE INTO Sanctum.CharacterStats AS target
    USING (SELECT @CharacterID AS CharacterID, @StatID AS StatID) AS source
    ON (target.CharacterID = source.CharacterID AND target.StatID = source.StatID)
    WHEN MATCHED THEN
        UPDATE SET 
            target.Base = @Base, 
            target.CustomMod = @CustomMod,
            target.OverrideValue = @OverrideValue,
            target.DoOverride = @DoOverride
    WHEN NOT MATCHED THEN
        INSERT (CharacterID, StatID, Base, CustomMod, OverrideValue, DoOverride)
        VALUES (@CharacterID, @StatID, @Base, @CustomMod, @OverrideValue, @DoOverride);
END