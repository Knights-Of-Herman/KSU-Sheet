DROP PROCEDURE IF EXISTS CreateJournalEntry
GO
CREATE PROCEDURE CreateJournalEntry
    @CharacterID INT,
    @Category INT
AS
BEGIN
    INSERT INTO Sanctum.CharacterJournal (CharacterID, Category)
    OUTPUT INSERTED.JournalID, INSERTED.Title, INSERTED.Content, INSERTED.Category, INSERTED.CreateDate
    VALUES (@CharacterID, @Category);
END
GO
DROP PROCEDURE IF EXISTS GetJournalEntries
GO
CREATE PROCEDURE GetJournalEntries
    @CharacterID INT
AS
BEGIN
    SELECT JournalID, Title, Content, Category, CreateDate
    FROM Sanctum.CharacterJournal
    WHERE CharacterID = @CharacterID;
END
GO
DROP PROCEDURE IF EXISTS SaveJournalEntry
GO
CREATE PROCEDURE SaveJournalEntry
    @JournalID INT,
    @Title NVARCHAR(255),
    @Content NVARCHAR(3000)
AS
BEGIN
    UPDATE Sanctum.CharacterJournal
    SET Title = @Title,
        Content = @Content
    WHERE JournalID = @JournalID;
END
GO
DROP PROCEDURE IF EXISTS DeleteJournalEntry
GO
CREATE PROCEDURE DeleteJournalEntry
    @JournalID INT
AS
BEGIN
    DELETE FROM Sanctum.CharacterJournal
    WHERE JournalID = @JournalID;
END
GO
