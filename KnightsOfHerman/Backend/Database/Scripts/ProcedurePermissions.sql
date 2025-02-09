-- User Procedures
GRANT EXECUTE ON [dbo].[CheckForEmail] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[CheckForUsername] TO [knightsofherman-backend];

-- Character Procedures
GRANT EXECUTE ON [dbo].[CreateBlankCharacter] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterCount] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterProfiles] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterPermissions] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[DeleteCharacter] TO [knightsofherman-backend];

--Abilities
GRANT EXECUTE ON [dbo].[CreateCharacterAbility] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterAbilities] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterAbility] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[DeleteCharacterAbility] TO [knightsofherman-backend];

--Armor
GRANT EXECUTE ON [dbo].[CreateCharacterArmor] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterArmor] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterArmor] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[DeleteCharacterArmor] TO [knightsofherman-backend];

--BIO
GRANT EXECUTE ON [dbo].[GetCharacterBio] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterBio] TO [knightsofherman-backend];

--Share
GRANT EXECUTE ON [dbo].[GetCharacterShareInfo] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[ShareCharacter] TO [knightsofherman-backend];

--Items
GRANT EXECUTE ON [dbo].[CreateCharacterItem] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterItems] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterItem] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[DeleteCharacterItem] TO [knightsofherman-backend];

--Journal
GRANT EXECUTE ON [dbo].[CreateJournalEntry] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetJournalEntries] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveJournalEntry] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[DeleteJournalEntry] TO [knightsofherman-backend];

-- Resources
GRANT EXECUTE ON [dbo].[CreateCharacterResources] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterResources] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterResource] TO [knightsofherman-backend];

--Stats
GRANT EXECUTE ON [dbo].[CreateCharacterStats] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterStats] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterStat] TO [knightsofherman-backend];

--Weapons
GRANT EXECUTE ON [dbo].[CreateCharacterWeapon] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[GetCharacterWeapons] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[SaveCharacterWeapon] TO [knightsofherman-backend];
GRANT EXECUTE ON [dbo].[DeleteCharacterWeapon] TO [knightsofherman-backend];