DROP PROCEDURE IF EXISTS GetCharacterBio
GO
CREATE PROCEDURE GetCharacterBio
    @CharacterID INT
AS
BEGIN
    SELECT 
        c.Name,
        c.Race,
        c.Background,
        c.CampaignID,
        c.TotalXP,
        c.UnspentXP,
        c.Destiny,
        c.Conflict,
        c.Languages,
        ISNULL(cmp.Name, 'No Campaign') AS Campaign
    FROM Sanctum.Characters c
    LEFT JOIN Sanctum.Campaigns cmp ON c.CampaignID = cmp.CampaignID
    WHERE CharacterID = @CharacterID
END
GO
DROP PROCEDURE IF EXISTS SaveCharacterBio
GO
CREATE PROCEDURE SaveCharacterBio
    @CharacterID INT,
    @Name NVARCHAR(255),
    @Race NVARCHAR(255),
    @Background NVARCHAR(255),
    @TotalXP INT,
    @UnspentXP INT,
    @Destiny TINYINT,
    @Conflict TINYINT,
    @Languages NVARCHAR(500)
AS
BEGIN
    UPDATE Sanctum.Characters
    SET
        Name = @Name,
        Race = @Race,
        Background = @Background,
        TotalXP = @TotalXP,
        UnspentXP = @UnspentXP,
        Destiny = @Destiny,
        Conflict = @Conflict,
        Languages = @Languages
    WHERE CharacterID = @CharacterID
END