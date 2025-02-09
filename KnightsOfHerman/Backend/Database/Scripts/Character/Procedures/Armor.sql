DROP PROCEDURE IF EXISTS CreateCharacterArmor;
GO
CREATE PROCEDURE CreateCharacterArmor
    @CharacterID INT
AS
BEGIN
    INSERT INTO Sanctum.CharacterArmor (CharacterID)
    OUTPUT INSERTED.ItemID, INSERTED.Name, INSERTED.Description, INSERTED.Weight, INSERTED.Hindrance, INSERTED.Equipped, INSERTED.Layer, INSERTED.Slot, INSERTED.Bludgeoning, INSERTED.Piercing, INSERTED.Slashing
    VALUES (@CharacterID);
END
GO

DROP PROCEDURE IF EXISTS GetCharacterArmor;
GO
CREATE PROCEDURE GetCharacterArmor
    @CharacterID INT
AS
BEGIN
    SELECT ItemID, CharacterID, Name, Description, Weight, Hindrance, Equipped, Layer, Slot, Bludgeoning, Piercing, Slashing
    FROM Sanctum.CharacterArmor
    WHERE CharacterID = @CharacterID;
END
GO

DROP PROCEDURE IF EXISTS SaveCharacterArmor;
GO
CREATE PROCEDURE SaveCharacterArmor
    @ItemID INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @Weight DECIMAL(6,2),
    @Hindrance TINYINT,
    @Equipped BIT,
    @Layer INT,
    @Slot INT,
    @Bludgeoning TINYINT,
    @Piercing TINYINT,
    @Slashing TINYINT
AS
BEGIN
    UPDATE Sanctum.CharacterArmor
    SET Name = @Name,
        Description = @Description,
        Weight = @Weight,
        Hindrance = @Hindrance,
        Equipped = @Equipped,
        Layer = @Layer,
        Slot = @Slot,
        Bludgeoning = @Bludgeoning,
        Piercing = @Piercing,
        Slashing = @Slashing
    WHERE ItemID = @ItemID;
END
GO

DROP PROCEDURE IF EXISTS DeleteCharacterArmor;
GO
CREATE PROCEDURE DeleteCharacterArmor
    @ItemID INT
AS
BEGIN
    DELETE FROM Sanctum.CharacterArmor 
    WHERE ItemID = @ItemID;
END
GO