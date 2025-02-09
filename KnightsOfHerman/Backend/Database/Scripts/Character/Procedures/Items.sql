DROP PROCEDURE IF EXISTS CreateCharacterItem;
GO

CREATE PROCEDURE CreateCharacterItem
    @CharacterID INT
AS
BEGIN
    INSERT INTO Sanctum.CharacterItems (CharacterID)
    OUTPUT INSERTED.ItemID, INSERTED.Name, INSERTED.Description, INSERTED.Weight, INSERTED.Quantity
    VALUES (@CharacterID);
END
GO
DROP PROCEDURE IF EXISTS GetCharacterItems;
GO
CREATE PROCEDURE GetCharacterItems
    @CharacterID INT
AS
BEGIN
    SELECT ItemID, CharacterID, Name, Description, Weight, Quantity
    FROM Sanctum.CharacterItems
    WHERE CharacterID = @CharacterID;
END
GO
DROP PROCEDURE IF EXISTS SaveCharacterItem;
GO
CREATE PROCEDURE SaveCharacterItem
    @ItemID INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @Quantity SMALLINT,
    @Weight DECIMAL(6,2)
AS
BEGIN
    UPDATE Sanctum.CharacterItems
    SET Name = @Name,
        Description = @Description,
        Quantity = @Quantity,
        Weight = @Weight
    WHERE ItemID = @ItemID;
END
GO
DROP PROCEDURE IF EXISTS DeleteCharacterItem;
GO
CREATE PROCEDURE DeleteCharacterItem
    @ItemID INT
AS
BEGIN
    DELETE FROM Sanctum.CharacterItems 
    WHERE ItemID = @ItemID;
END