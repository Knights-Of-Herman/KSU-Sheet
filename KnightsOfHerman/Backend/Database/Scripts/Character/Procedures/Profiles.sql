DROP PROCEDURE IF EXISTS GetCharacterProfiles;
GO
CREATE PROCEDURE GetCharacterProfiles
    @UserID INT
AS
BEGIN
    SELECT 
        c.CharacterID,
        c.Name,
        c.CampaignID,
        c.Race,
        c.TotalXP,
        ISNULL(cmp.Name, 'No Campaign') AS Campaign, 
        1 AS AccessLevel 
    FROM Sanctum.Characters c
    LEFT JOIN Sanctum.Campaigns cmp ON c.CampaignID = cmp.CampaignID
    WHERE c.OwnerID = @UserID 

    UNION ALL 

    SELECT 
        c.CharacterID,
        c.Name,
        c.CampaignID,
        c.Race,
        c.TotalXP,
        ISNULL(cmp.Name, 'No Campaign') AS Campaign, 
        sc.AccessLevel  
    FROM Sanctum.Characters c
    JOIN Sanctum.SharedCharacters sc ON c.CharacterID = sc.CharacterID
    LEFT JOIN Sanctum.Campaigns cmp ON c.CampaignID = cmp.CampaignID
    WHERE sc.UserID = @UserID

END
