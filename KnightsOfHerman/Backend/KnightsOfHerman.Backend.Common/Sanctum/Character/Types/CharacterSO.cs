using KnightsOfHerman.Backend.Common.Database.Types.Character;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Types
{
    /// <summary>
    /// Server Side Character Representation
    /// </summary>
    public class CharacterSO : ITrackModifed
    {

        public Dictionary<int, CharacterAccess> Permissions { get; set; } = new();
        public bool Modified { get; set; }

        public int CharacterID { get; }
        public Dictionary<int,CharacterWeaponSO> Weapons { get; set; } = new();

        public Dictionary<int,CharacterItemSO> Items { get; set; } = new();

        public Dictionary<int,CharacterArmorSO> Armor { get; set; } = new();

        public Dictionary<int,CharacterJournalSO> Journal { get; set; } = new();
        public Dictionary<int,CharacterAbilitySO> Abilities { get; set; } = new();

        public CharacterBioSO Bio { get; set; } = new();

        public Dictionary<CharacterStats, CharacterStatSO> Stats { get; set; } = new();
        public Dictionary<CharacterResources, CharacterResourceSO> Resources { get; set; } = new();

        public CharacterSO(int id)
        {
            CharacterID = id;
        }

        public TryResult<CharacterDTO> ToDTO(int userID)
        {
            if(Permissions.ContainsKey(userID) && Permissions[userID] != CharacterAccess.None)
            {

                CharacterDTO dto = new();

                dto.CharacterID = CharacterID;

                dto.Access = Permissions[userID];

                dto.Stats = Stats.ToDictionary(x => x.Key, x => new CharacterStatDTO(x.Value));
                dto.Resources = Resources.ToDictionary(x => x.Key, x => new CharacterResourceDTO(x.Value));
                dto.Weapons = Weapons.ToDictionary(x => x.Key, x => new CharacterWeaponDTO(x.Value));
                dto.Items = Items.ToDictionary(x => x.Key, x => new CharacterItemDTO(x.Value));
                dto.Armor = Armor.ToDictionary(x => x.Key, x => new CharacterArmorDTO(x.Value));
                dto.Journal = Journal.ToDictionary(x => x.Key, x => new CharacterJournalDTO(x.Value));
                dto.Abilities = Abilities.ToDictionary(x => x.Key, x => new CharacterAbilityDTO(x.Value));
                dto.Bio = new CharacterBioDTO(Bio);

                return TryResult<CharacterDTO>.Success(dto);
            }
            else
            {
                return TryResult<CharacterDTO>.Fail("No Permission");
            }
        }
    }
}
