DROP PROCEDURE IF EXISTS CreateCharacterWeapon;
GO
CREATE PROCEDURE CreateCharacterWeapon
    @CharacterID INT
AS
BEGIN
    INSERT INTO Sanctum.CharacterWeapons (CharacterID)
    OUTPUT INSERTED.ItemID, INSERTED.Name, INSERTED.Description, INSERTED.Weight, INSERTED.Quantity, INSERTED.Damage, INSERTED.Accuracy
    VALUES (@CharacterID);
END
GO
DROP PROCEDURE IF EXISTS GetCharacterWeapons
GO
CREATE PROCEDURE GetCharacterWeapons
    @CharacterID INT
AS
BEGIN
    SELECT ItemID, CharacterID, Name, Description, Weight, Quantity, Damage, Accuracy
    FROM Sanctum.CharacterWeapons
    WHERE CharacterID = @CharacterID;
END
GO
DROP PROCEDURE IF EXISTS SaveCharacterWeapon;
GO
CREATE PROCEDURE SaveCharacterWeapon
    @ItemID INT,
    @Name NVARCHAR(255),
    @Description NVARCHAR(1000),
    @Quantity SMALLINT,
    @Weight DECIMAL(6,2),
    @Damage NVARCHAR(100),
    @Accuracy SMALLINT
AS
BEGIN
    UPDATE Sanctum.CharacterWeapons
    SET Name = @Name,
        Description = @Description,
        Quantity = @Quantity,
        Weight = @Weight,
        Damage = @Damage,
        Accuracy = @Accuracy
    WHERE ItemID = @ItemID;
END
GO
DROP PROCEDURE IF EXISTS DeleteCharacterWeapon;
GO
CREATE PROCEDURE DeleteCharacterWeapon
    @ItemID INT
AS
BEGIN
    DELETE FROM Sanctum.CharacterWeapons 
    WHERE ItemID = @ItemID;
END
GO