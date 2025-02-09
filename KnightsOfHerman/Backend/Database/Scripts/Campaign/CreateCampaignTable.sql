CREATE TABLE Sanctum.Campaigns (
    -- Database Info
    CampaignID INT IDENTITY(1,1) PRIMARY KEY,
    OwnerID INT FOREIGN KEY (OwnerID) REFERENCES KOH.Users(UserID),

    -- Campaign Info
    Name NVARCHAR(255) DEFAULT 'New Campaign'
)
