using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Collections;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Stats;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Client Character Object
    /// </summary>
    public class CharacterCO : INotifyModificationPath
    {
        private bool _silent;
        public int CharacterID { get; private set; }

        //public CharacterDTO Character { get; set; } //This is here just for testing

        public readonly DerivedAttributes DerivedStats;
        public CharacterEquipedArmor EquipedArmor { get; private set; }
        public TrackedDictionary<int, CharacterItemCO> Items { get; private set; }
        public TrackedDictionary<int, CharacterWeaponCO> Weapons { get; private set; }
        public TrackedDictionary<int, CharacterArmorCO> Armor { get; private set; }
        public TrackedDictionary<int, CharacterJournalCO> Journal { get; private set; }
        public TrackedDictionary<int, CharacterAbilityCO> Abilities { get; private set; }

        public TrackedDictionary<CharacterStats, CharacterStatCO> Stats { get; private set; }
        public TrackedDictionary<CharacterResources, CharacterResourceCO> Resources { get; private set; }

        public CharacterBioCO Bio { get; private set; }

        public bool Modified { get; set; }

        public event TrackedModificationEventHandler? OnTrackedModification;

        public CharacterCO(CharacterDTO dto)
        {
            //Character = dto;
            //eventualls set these up to run concurrently using a List<tasks> and Task.waitall()


            EquipedArmor = new();

            Items = new TrackedDictionary<int, CharacterItemCO>();
            foreach (var item in dto.Items)
            {
                Items[item.Key] = new CharacterItemCO(item.Value);
            }

            Weapons = new TrackedDictionary<int, CharacterWeaponCO>();
            foreach (var weapon in dto.Weapons)
            {
                Weapons[weapon.Key] = new CharacterWeaponCO(weapon.Value);
            }

            Armor = new TrackedDictionary<int, CharacterArmorCO>();
            foreach (var arm in dto.Armor)
            {
                var armor = new CharacterArmorCO(arm.Value);
                Armor[arm.Key] = armor;
                if (arm.Value.Equipped)
                {
                    if (!EquipedArmor.TryEquipArmor(armor).IsSuccess)
                    {
                        armor.Equipped = false; //Fix armor being equipped when it shouldn't be.
                    }
                }
            }

            Journal = new TrackedDictionary<int, CharacterJournalCO>();
            foreach (var entry in dto.Journal)
            {
                Journal[entry.Key] = new CharacterJournalCO(entry.Value);
            }

            Abilities = new TrackedDictionary<int, CharacterAbilityCO>();
            foreach (var ability in dto.Abilities)
            {
                Abilities[ability.Key] = new CharacterAbilityCO(ability.Value);
            }

            Stats = new TrackedDictionary<CharacterStats, CharacterStatCO>();
            foreach (var stat in dto.Stats)
            {
                Stats[stat.Key] = new CharacterStatCO(stat.Value);
            }

            Resources = new TrackedDictionary<CharacterResources, CharacterResourceCO>();
            foreach (var resource in dto.Resources)
            {
                Resources[resource.Key] = new CharacterResourceCO(resource.Value);
            }
            Bio = new CharacterBioCO(dto.Bio);
            DerivedStats = new(this);
            Subscribe();
        }

        private void Subscribe()
        {
            Items.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Items), y);
            Weapons.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Weapons), y);
            Armor.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Armor), y);
            Journal.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Journal), y);
            Abilities.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Abilities), y);
            Stats.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Stats), y);
            Resources.OnTrackedModification += (x, y) => OnDictionaryChange(nameof(Resources), y);
            Bio.OnTrackedModification += OnBioChanged;
        }

        void OnBioChanged(object prev, TrackedModificationEventArgs args)
        {
            if (!_silent)
            {
                args.PrependPath(nameof(Bio));
                OnTrackedModification?.Invoke(this, args);
            }

        }

        //Special Case for armor.
        void OnArmorChange(object prev, TrackedModificationEventArgs args)
        {
            //Try Equip Armor
            if (args.Path.Contains("Equipped"))
            {
                if (args.Value is bool)
                {
                    //Try equip.
                }
                //Try to equip before sending.
            }
        }

        void OnDictionaryChange(string Dictionary, TrackedModificationEventArgs args)
        {
            if (!_silent)
            {
                args.PrependPath(Dictionary);
                OnTrackedModification?.Invoke(this, args);
            }
        }

        public TryResult ModifyByPath(TrackedModificationEventArgs args)
        {
            try
            {
                _silent = true;
                return ModifyCharacterRecursive(this, args.Path, args.Value, args.Action);
            }
            finally
            {
                _silent = false;
            }
        }

        TryResult ModifyCharacterRecursive(object current, string path, object value, EditActions action)
        {
            try
            {
                string[] paths = path.Split(".", 2);
                if (paths.Count() == 2)
                {
                    //use system.refletion to obtain the new current
                    object next;
                    if (current is IDictionary dict)
                    {
                        object key;
                        //Get the type of key and convert the path to it.
                        Type Tkey = current.GetType().GetGenericArguments()[0];
                        if (Tkey.IsEnum)
                        {
                            key = Enum.Parse(Tkey, paths[0]);
                        }
                        else
                        {
                            key = Convert.ChangeType(paths[0], Tkey);
                        }
                        if (!dict.Contains(key))
                        {
                            return TryResult.Fail($"Key {paths[0]} not in dictionary {current.GetType()}");
                        }
                        next = dict[key];
                    }
                    else
                    {
                        PropertyInfo info = current.GetType().GetProperty(paths[0]);
                        if (info == null) return TryResult.Fail($"Property {paths[0]} not found in {current.GetType()}");
                        next = info.GetValue(current);
                    }
                    if (next == null) return TryResult.Fail($"Property {paths[0]} null in {current.GetType()}");
                    return ModifyCharacterRecursive(next, paths[1], value, action);
                }
                else
                {
                    if (current is IDictionary dict) //Dictionary Works differently.
                    {
                        if (action == EditActions.Clear)
                        {
                            //Delete all from DB
                            throw new NotImplementedException("Clear is not implemented");
                            dict.Clear();
                            return TryResult.Success();
                        }
                        else
                        {
                            Type Tkey = current.GetType().GetGenericArguments()[0];
                            var key = Convert.ChangeType(paths[0], Tkey);
                            Type TValue = current.GetType().GetGenericArguments()[1];
                            object val = Convert.ChangeType(value, TValue);

                            if (action == EditActions.Remove)
                            {
                                //Get Type.
                                if (dict.Contains(key))
                                {
                                    dict.Remove(key);
                                    return TryResult.Success();
                                }
                                return TryResult.Fail($"Key ({key}) does not exist");
                            }
                            else
                            {
                                dict[key] = val;
                                return TryResult.Success();

                            }
                        }
                    }
                    else
                    {
                        //Only edits are allowed here.
                        PropertyInfo info = current.GetType().GetProperty(paths[0]);
                        if (info == null) return TryResult.Fail($"Property {paths[0]} not found in {current.GetType()}");

                        if (info.PropertyType.IsEnum)
                        {
                            var val = Enum.ToObject(info.PropertyType, value);
                            info.SetValue(current, val);
                        }
                        else
                        {
                            var val = Convert.ChangeType(value, info.PropertyType);
                            info.SetValue(current, val);
                        }

                        return TryResult.Success();
                    }
                }
            }
            catch (Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
        }

   
    }


    public class DerivedAttributes
    {
        private readonly CharacterCO _character;

        internal DerivedAttributes(CharacterCO character)
        {
            _character = character;
        }

        public int Recovery => _character.Stats[CharacterStats.Resolve].Total + _character.Stats[CharacterStats.Constitution].Total;

        public int ConflictThreshold => Math.Max(5 + _character.Stats[CharacterStats.Resolve].Total - _character.Stats[CharacterStats.Intelligence].Total,1);

        public int Memory => (_character.Stats[CharacterStats.Intelligence].Total + _character.Stats[CharacterStats.Reflex].Total) / 2;

        public int UsedMemory => (_character.Abilities.Where(x => x.Value.Memorized)).Count();

        public int MeleeBonus => (int)Math.Ceiling(_character.Stats[CharacterStats.Strength].Total / 2.0m);

        public decimal CarryingCapacity => _character.Stats[CharacterStats.Strength].Total * 10;

        public decimal UsedCarryingCapacity => _character.Weapons.Values.Sum(x => x.Weight * x.Quantity) + _character.Items.Values.Sum(x=> (x.Weight * x.Quantity)) + _character.Armor.Values.Sum(x=> x.Equipped ? x.Weight/2 : x.Weight);

        public int Throw => (_character.Stats[CharacterStats.Strength].Total / (_character.Stats[CharacterStats.Physique].Total / 2)) * 10;

        public int SwimSpeed => Math.Max(_character.Resources[CharacterResources.Speed].Total / 10, 1);

        public int RunningJump => _character.Resources[CharacterResources.Speed].Total;
        public int StandingJump => _character.Resources[CharacterResources.Speed].Total / 4;

        public int Initiative => _character.Stats[CharacterStats.Reflex].Total;

        public int Hindrance => _character.Armor.Values.Sum(x => x.Equipped ? x.Hindrance : 0);

    }
}
