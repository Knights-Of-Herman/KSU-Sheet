-- Call this only on Character Creation
DROP PROCEDURE IF EXISTS CreateCharacterResources
GO
CREATE PROCEDURE CreateCharacterResources
    @CharacterID INT,
    @ResourceIDs NVARCHAR(MAX)
AS
BEGIN
    -- Split the @StatIDs string and insert each stat into Sanctum.CharacterStats
    INSERT INTO Sanctum.CharacterResources (CharacterID, ResourceID)
    SELECT @CharacterID, value
    FROM STRING_SPLIT(@ResourceIDs, ',') AS Resources
    WHERE TRY_CONVERT(INT, Resources.value) IS NOT NULL;
END
GO
DROP PROCEDURE IF EXISTS GetCharacterResources
GO
CREATE PROCEDURE GetCharacterResources
    @CharacterID INT
AS
BEGIN
    SELECT 
        ResourceID,
        Attribute,
        Modifier,
        OverrideMaxValue,
        DoOverrideMax,
        CurrentValue
    FROM Sanctum.CharacterResources
    WHERE CharacterID = @CharacterID
END
GO
DROP PROCEDURE IF EXISTS SaveCharacterResource
GO
CREATE PROCEDURE SaveCharacterResource
    @CharacterID INT,
    @ResourceID INT,
    @Attribute SMALLINT,
    @Modifier SMALLINT,
    @OverrideMaxValue SMALLINT,
    @DoOverrideMax BIT,
    @CurrentValue SMALLINT
AS
BEGIN
    MERGE INTO Sanctum.CharacterResources AS target
    USING (SELECT @CharacterID AS CharacterID, @ResourceID AS ResourceID) AS source
    ON (target.CharacterID = source.CharacterID AND target.ResourceID = source.ResourceID)
    WHEN MATCHED THEN
        UPDATE SET 
            target.Attribute = @Attribute, 
            target.Modifier = @Modifier,
            target.OverrideMaxValue = @OverrideMaxValue,
            target.DoOverrideMax = @DoOverrideMax,
            target.CurrentValue = @CurrentValue
    WHEN NOT MATCHED THEN
        INSERT (CharacterID, ResourceID, Attribute, Modifier, OverrideMaxValue, DoOverrideMax, CurrentValue)
        VALUES (@CharacterID, @ResourceID, @Attribute, @Modifier, @OverrideMaxValue, @DoOverrideMax, @CurrentValue);
END
GO