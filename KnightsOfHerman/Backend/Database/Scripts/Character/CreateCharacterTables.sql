CREATE TABLE Sanctum.Characters (
    -- Database Info
    CharacterID INT IDENTITY(1,1) PRIMARY KEY,
    OwnerID INT FOREIGN KEY (OwnerID) REFERENCES KOH.Users(UserID), 
    CampaignID INT DEFAULT -1, 

    -- Basic Info
     Name NVARCHAR(255) DEFAULT 'New Adventurer',
     Race NVARCHAR(255) DEFAULT 'Unknown Race',
     Background NVARCHAR(255) DEFAULT 'Unknown Background',
     Conflict TINYINT DEFAULT 0, 
     Languages NVARCHAR(500) DEFAULT '',
     -- Experience 
     TotalXP INT DEFAULT 0,
     UnspentXP INT DEFAULT 0,
     Destiny TINYINT DEFAULT 0,
)
GO
CREATE TABLE Sanctum.SharedCharacters(
    CharacterID INT REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    UserID INT REFERENCES KOH.Users(UserID) ON DELETE CASCADE,
    AccessLevel INT NOT NULL CHECK (AccessLevel <> 1) DEFAULT 0, -- SharedCharacters cannot have Ownership Access Level.
    PRIMARY KEY(CharacterID, UserID)
)
GO
CREATE TABLE Sanctum.CharacterStats (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    StatID INT,
    Base SMALLINT DEFAULT 0,
    CustomMod SMALLINT DEFAULT 0,
    OverrideValue SMALLINT DEFAULT 0,
    DoOverride BIT DEFAULT 0,
    PRIMARY KEY (CharacterID, StatID),
)
GO
CREATE TABLE Sanctum.CharacterResources (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    ResourceID INT,
    Attribute SMALLINT DEFAULT 0,
    Modifier SMALLINT DEFAULT 0,
    OverrideMaxValue SMALLINT DEFAULT 0,
    DoOverrideMax BIT DEFAULT 0,
    CurrentValue SMALLINT DEFAULT 0,
    PRIMARY KEY (CharacterID, ResourceID)
)
GO
CREATE TABLE Sanctum.CharacterJournal (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    JournalID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) DEFAULT 'New Entry',
    Content NVARCHAR(3000) DEFAULT '',
    Category INT,
    CreateDate DATE DEFAULT GETDATE()
)
GO 
CREATE TABLE Sanctum.CharacterAbilities (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    AbilityID INT IDENTITY(1,1) PRIMARY KEY,
    AbilityType INT DEFAULT 0,
    Title NVARCHAR(255) DEFAULT 'New Entry',
    Content NVARCHAR(3000) DEFAULT '',
    Cost NVARCHAR(255) DEFAULT '',
    Memorized BIT DEFAULT 0
)
GO 
CREATE TABLE Sanctum.CharacterItems (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) DEFAULT 'New Item',
    Description NVARCHAR(1000) DEFAULT '',
    Weight DECIMAL(6,2) DEFAULT 0.0,
    Quantity SMALLINT DEFAULT 1,
)
GO
CREATE TABLE Sanctum.CharacterWeapons (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) DEFAULT 'New Weapon',
    Description NVARCHAR(1000) DEFAULT '',
    Weight DECIMAL(6,2) DEFAULT 0.0,
    Quantity SMALLINT DEFAULT 1,
    Damage NVARCHAR(100) DEFAULT '1d4',
    Accuracy SMALLINT DEFAULT 0
)
GO
CREATE TABLE Sanctum.CharacterArmor (
    CharacterID INT FOREIGN KEY (CharacterID) REFERENCES Sanctum.Characters(CharacterID) ON DELETE CASCADE,
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) DEFAULT 'New Armor',
    Description NVARCHAR(1000) DEFAULT '',
    Weight DECIMAL(6,2) DEFAULT 0.0,
    Hindrance TINYINT DEFAULT 0,
    Equipped BIT DEFAULT 0,
    Layer INT DEFAULT 0,
    Slot INT DEFAULT 1,
    Bludgeoning TINYINT DEFAULT 0,
    Piercing TINYINT DEFAULT 0,
    Slashing TINYINT DEFAULT 0,
)
GO
CREATE TABLE Sanctum.Effects (
    EffectID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) DEFAULT 'New Effect',
    Description NVARCHAR(1000) DEFAULT ''
)
GO
CREATE TABLE Sanctum.WeaponEffects(
    WeaponID INT FOREIGN KEY (WeaponID) REFERENCES Sanctum.CharacterWeapons(ItemID) ON DELETE CASCADE,
    EffectID INT FOREIGN KEY (EffectID) REFERENCES Sanctum.Effects(EffectID) 
)
CREATE TABLE Sanctum.ArmorEffects(
    ArmorID INT FOREIGN KEY (ArmorID) REFERENCES Sanctum.CharacterArmor(ItemID) ON DELETE CASCADE,
    EffectID INT FOREIGN KEY (EffectID) REFERENCES Sanctum.Effects(EffectID) 
)
