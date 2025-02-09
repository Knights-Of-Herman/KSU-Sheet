using KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Types;
using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Abstract.Modification;
using KnightsOfHerman.Common.Collections;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Abilities;
using KnightsOfHerman.Common.Sanctum.Abstract.Character.Notes;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using KnightsOfHerman.Common.Types;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Sanctum.Character.Services
{
    /// <summary>
    /// Character Main Logic Implementation
    /// </summary>
    public class CharacterService : ICharacterService
    {
        ICharacterDBService _db;

        ICharacterCache _cache;

        ICharacterNotificationService _notify;

        ICharacterLockService _locks;

        public CharacterService(ICharacterCache cache, ICharacterDBService db, ICharacterNotificationService notify, ICharacterLockService locks)
        {
            _db = db;
            _cache = cache;
            _locks = locks;
            _notify = notify;
        }

        bool HasEditAccess(CharacterAccess access)
        {
            return access == CharacterAccess.Owner || access == CharacterAccess.Editor;
        }

        /// <summary>
        /// Lock before using this
        /// </summary>
        async Task<TryResult<CharacterSO>> TryGetCharacter(int characterid)
        {
            try
            {
                CharacterSO? character = null;
                var cacheResult = await _cache.TryLoadCharacter(characterid);
                if (cacheResult.IsSuccess && cacheResult.Value != null)
                {
                    character = cacheResult.Value;
                }
                else
                {
                    var dbResult = await _db.GetCharacterFromDatabase(characterid);
                    if (dbResult.IsSuccess && dbResult.Value != null)
                    {
                        character = dbResult.Value;
                        await _cache.TrySaveCharacter(character);
                    }
                    else
                    {
                        return TryResult<CharacterSO>.Fail(dbResult.ErrorMessage);
                    }
                }
                return TryResult<CharacterSO>.Success(character);
            }
            catch (Exception ex)
            {
                return TryResult<CharacterSO>.Fail(ex.Message);
            }
        }

        public async Task<TryResult<CharacterAbilityDTO>> CreateAbility(int userID, int characterID, AbilityType type)
        {
            //Check if userID is owner or editor

            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync(); //Wait for sempaphore to open
            try
            {
                //Load character
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult<CharacterAbilityDTO>.Fail(loadresult.ErrorMessage);
#else
                    return TryResult<CharacterAbilityDTO>.Fail("Server Error"); 
#endif
                }
                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && HasEditAccess(character.Permissions[userID]))
                {
                    //Now we can edit
                    var result = await _db.CreateAbility(characterID, type);
                    if (result.IsSuccess && result.Value != null)
                    {
                        character.Abilities[result.Value.AbilityID] = result.Value;
                        await _cache.TrySaveCharacter(character);
                        return TryResult<CharacterAbilityDTO>.Success(new CharacterAbilityDTO(result.Value));
                    }
                    else
                    {
#if DEBUG
                        return TryResult<CharacterAbilityDTO>.Fail(result.ErrorMessage);
#else
                        return TryResult<CharacterAbilityDTO>.Fail("Server Error"); ;
#endif
                    }

                }
                else
                {
                    return TryResult<CharacterAbilityDTO>.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<CharacterAbilityDTO>.Fail("Server Error"); ;
#else
                    return TryResult<CharacterAbilityDTO>.Fail(ex.Message);
#endif
            }
            finally
            {
                //Release sempahore even if exception
                charLock.Release();
            }

            //Load Character

            //Create Ability

            //Save Ability

            //Returns
            throw new NotImplementedException();
        }

        public async Task<TryResult<CharacterArmorDTO>> CreateArmor(int userID, int characterID)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult<CharacterArmorDTO>.Fail(loadresult.ErrorMessage);
#else
                    return TryResult<CharacterArmorDTO>.Fail("Server Error");
#endif
                }
                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && HasEditAccess(character.Permissions[userID]))
                {
                    var result = await _db.CreateArmor(characterID);
                    if (result.IsSuccess && result.Value != null)
                    {
                        character.Armor[result.Value.ItemID] = result.Value;
                        await _cache.TrySaveCharacter(character);
                        return TryResult<CharacterArmorDTO>.Success(new CharacterArmorDTO(result.Value));
                    }
                    else
                    {
#if DEBUG
                        return TryResult<CharacterArmorDTO>.Fail(result.ErrorMessage);
#else
                        return TryResult<CharacterArmorDTO>.Fail("Server Error");
#endif
                    }
                }
                else
                {
                    return TryResult<CharacterArmorDTO>.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<CharacterArmorDTO>.Fail(ex.Message);
#else
                return TryResult<CharacterArmorDTO>.Fail("Server Error");
#endif
            }
            finally
            {
                charLock.Release();
            }
        }

        public async Task<TryResult<int>> CreateCharacter(int userID)
        {
            try
            {
                var countResult = await _db.GetCharacterCount(userID);
                if (countResult.IsSuccess)
                {
                    if (countResult.Value < 5)
                    {
                        //Can make more.
                        var result = await _db.CreateCharacter(userID);
                        if (result.IsSuccess)
                        {
                            return result;
                        }
                        else
                        {
#if DEBUG
                            return result;
#else
                            return TryResult<int>.Fail("Server Error");
#endif                           
                        }
                    }
                    else
                    {
                        //Too many
                        return TryResult<int>.Fail("Exceeded Amount of Characters");
                    }
                }
                else
                {
#if DEBUG
                    return TryResult<int>.Fail(countResult.ErrorMessage);
#else
                    return TryResult<int>.Fail("Server Error");
#endif
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<int>.Fail(ex.Message);
#else
                return TryResult<int>.Fail("Server Error");
#endif
            }
        }

        public async Task<TryResult<CharacterItemDTO>> CreateItem(int userID, int characterID)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult<CharacterItemDTO>.Fail(loadresult.ErrorMessage);
#else
                    return TryResult<CharacterItemDTO>.Fail("Server Error");
#endif
                }
                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && HasEditAccess(character.Permissions[userID]))
                {
                    var result = await _db.CreateItem(characterID);
                    if (result.IsSuccess && result.Value != null)
                    {
                        character.Items[result.Value.ItemID] = result.Value;
                        await _cache.TrySaveCharacter(character);
                        return TryResult<CharacterItemDTO>.Success(new CharacterItemDTO(result.Value));
                    }
                    else
                    {
#if DEBUG
                        return TryResult<CharacterItemDTO>.Fail(result.ErrorMessage);
#else
                        return TryResult<CharacterItemDTO>.Fail("Server Error");
#endif
                    }
                }
                else
                {
                    return TryResult<CharacterItemDTO>.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<CharacterItemDTO>.Fail(ex.Message);
#else
                return TryResult<CharacterItemDTO>.Fail("Server Error");
#endif
            }
            finally
            {
                charLock.Release();
            }
        }

        public async Task<TryResult<CharacterJournalDTO>> CreateJournal(int userID, int characterID, JournalCategory category)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult<CharacterJournalDTO>.Fail(loadresult.ErrorMessage);
#else
                    return TryResult<CharacterJournalDTO>.Fail("Server Error");
#endif
                }
                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && HasEditAccess(character.Permissions[userID]))
                {
                    var result = await _db.CreateJournal(characterID, category);
                    if (result.IsSuccess && result.Value != null)
                    {
                        character.Journal[result.Value.JournalID] = result.Value;
                        await _cache.TrySaveCharacter(character);
                        return TryResult<CharacterJournalDTO>.Success(new CharacterJournalDTO(result.Value));
                    }
                    else
                    {
#if DEBUG
                        return TryResult<CharacterJournalDTO>.Fail(result.ErrorMessage);
#else
                        return TryResult<CharacterJournalDTO>.Fail("Server Error");
#endif
                    }
                }
                else
                {
                    return TryResult<CharacterJournalDTO>.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<CharacterJournalDTO>.Fail(ex.Message);
#else
                return TryResult<CharacterJournalDTO>.Fail("Server Error");
#endif
            }
            finally
            {
                charLock.Release();
            }
        }

        public async Task<TryResult<CharacterWeaponDTO>> CreateWeapon(int userID, int characterID)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult<CharacterWeaponDTO>.Fail(loadresult.ErrorMessage);
#else
                    return TryResult<CharacterWeaponDTO>.Fail("Server Error");
#endif
                }
                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && HasEditAccess(character.Permissions[userID]))
                {
                    var result = await _db.CreateWeapon(characterID);
                    if (result.IsSuccess && result.Value != null)
                    {
                        character.Weapons[result.Value.ItemID] = result.Value;
                        await _cache.TrySaveCharacter(character);
                        return TryResult<CharacterWeaponDTO>.Success(new CharacterWeaponDTO(result.Value));
                    }
                    else
                    {
#if DEBUG
                        return TryResult<CharacterWeaponDTO>.Fail(result.ErrorMessage);
#else
                        return TryResult<CharacterWeaponDTO>.Fail("Server Error");
#endif
                    }
                }
                else
                {
                    return TryResult<CharacterWeaponDTO>.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<CharacterWeaponDTO>.Fail(ex.Message);
#else
                return TryResult<CharacterWeaponDTO>.Fail("Server Error");
#endif
            }
            finally
            {
                charLock.Release();
            }
        }

        public async Task<TryResult> DeleteCharacter(int userID, int characterID)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null) return loadresult;

                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && character.Permissions[userID] == CharacterAccess.Owner)
                {
                    var result = await _db.DeleteCharacter(userID, characterID);
                    if (result.IsSuccess)
                    {
                        _notify.NotifyCharacterDeletion(characterID);
                    }
#if DEBUG
                    return result;
#else
                    return TryResult.Fail("Server Error");
#endif
                }
                else
                {
                    return TryResult.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult.Fail(ex.Message);
#else
                return TryResult.Fail("Server Error");
#endif
            }
            finally
            {
                charLock.Release();
            }

        }

        public async Task<TryResult<CharacterDTO>> GetCharacterDTO(int userID, int characterID)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult<CharacterDTO>.Fail(loadresult.ErrorMessage);
#else
                    return TryResult<CharacterDTO>.Fail("Server Error");
#endif   
                }
                var character = loadresult.Value;
                return character.ToDTO(userID);
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult<CharacterDTO>.Fail(ex.Message);
#else
                return TryResult<CharacterDTO>.Fail("Server Error");
#endif            
            }
            finally
            {
                charLock.Release();
            }
        }

        public async Task<TryResult<List<CharacterProfile>>> GetProfiles(int userID)
        {
            var profilesResult = await _db.GetCharacterProfiles(userID);

            if (profilesResult.IsSuccess)
            {
                var profiles = profilesResult.Value;
                //Have updated profiles.
                foreach (var profile in profiles)
                {
                    var charLock = _locks.GetLock(profile.CharacterID);
                    await charLock.WaitAsync();
                    try
                    {
                        var cacheResult = await _cache.TryLoadCharacter(profile.CharacterID);
                        if (cacheResult.IsSuccess && cacheResult.Value != null)
                        {
                            profile.Name = cacheResult.Value.Bio.Name;
                            profile.Race = cacheResult.Value.Bio.Race;
                            profile.TotalXP = cacheResult.Value.Bio.TotalXP;
                        }
                    }
                    finally
                    {
                        charLock.Release();
                    }
                }
                return TryResult<List<CharacterProfile>>.Success(profiles);
            } else
            {
                return profilesResult;
            }
        }


        public async Task<TryResult> ModifyCharacter(int userID, int characterID, TrackedModificationEventArgs args)
        {
            //First convert args.Value to a usable object.
            if(args.Value is JsonElement json)
            {
                return TryResult.Fail("Raw JSON Not supported as a args.Value");
            }


            //Check if userID is owner or editor
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                var loadresult = await TryGetCharacter(characterID);
                if (!loadresult.IsSuccess || loadresult.Value == null)
                {
#if DEBUG
                    return TryResult.Fail(loadresult.ErrorMessage);
#else
                    return TryResult.Fail("Server Error");
#endif   
                }
                var character = loadresult.Value;

                if (character.Permissions.ContainsKey(userID) && HasEditAccess(character.Permissions[userID]))
                {
                    var result = await ModifyCharacterRecursive(characterID, character, args.Path, args.Value, args.Action);

                    if (result.IsSuccess)
                    {
                        character.Modified = true;
                        await _cache.TrySaveCharacter(character); //Save Changes
                        return TryResult.Success();
                    }
#if DEBUG
                    return TryResult.Fail(result.ErrorMessage);
#else
                    return TryResult.Fail("Server Error");
#endif                   
                } else
                {
                    return TryResult.Fail("No Permission");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                return TryResult.Fail(ex.Message);
#else
                return TryResult.Fail("Server Error");
#endif            
            }
            finally
            {
                charLock.Release();
            }
        }

        /// <summary>
        /// The recursive algorithm in charge of modifiyng a character
        /// </summary>
        /// <param name="characterid"></param>
        /// <param name="current">The current object</param>
        /// <param name="path">Path to the property, gets split each time</param>
        /// <param name="value"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        async Task<TryResult> ModifyCharacterRecursive(int characterid, object current, string path, object value, EditActions action)
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
                    return await ModifyCharacterRecursive(characterid, next, paths[1], value, action);
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


                            if (action == EditActions.Remove)
                            {
                                //Get Type.
                                if (dict.Contains(key))
                                {
                                    bool success = true;
                                    if (dict is Dictionary<int, CharacterJournalSO>)
                                    {
                                        var result = await _db.DeleteJournal((int)key);
                                        success = result.IsSuccess;
                                    }
                                    else if (dict is Dictionary<int, CharacterAbilitySO>)
                                    {
                                        var result = await _db.DeleteAbility((int)key);
                                        success = result.IsSuccess;
                                    }
                                    else if(dict is Dictionary<int, CharacterArmorSO>)
                                    {
                                        var result = await _db.DeleteArmor((int)key);
                                        success = result.IsSuccess;
                                    }
                                    else if(dict is Dictionary<int, CharacterWeaponSO>)
                                    {
                                        var result = await _db.DeleteWeapon((int)key);
                                        success = result.IsSuccess;
                                    }
                                    else if(dict is Dictionary<int, CharacterItemSO>)
                                    {
                                        var result = await _db.DeleteItem((int)key);
                                        success = result.IsSuccess;
                                    } else
                                    {
                                        success = false; //Other Removes not supported
                                    }
                                    if (success)
                                    {
                                        dict.Remove(key);
                                        return TryResult.Success();
                                    }
                                    else
                                    {
                                        return TryResult.Fail($"Failed to Remove {key}");
                                    }
                                }
                                return TryResult.Fail($"Key ({key}) does not exist");
                            }
                            else
                            {
                                Type TValue = current.GetType().GetGenericArguments()[1];
                                object val = Convert.ChangeType(value, TValue);
                                dict[key] = val; //Probably doesn't need to be implemented because the server handles the other addings elsewhere.
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
            } catch(Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
            
        }

        public async Task<CharacterShareResult> ShareCharacter(int userID, string shareUser, int characterID, CharacterAccess access)
        {
            try
            {
                //Can't have multiple owners
                if(access == CharacterAccess.Owner)
                {
                    return CharacterShareResult.NotAuthorized;
                }

                var charLock = _locks.GetLock(characterID);
                await charLock.WaitAsync();
                try
                {
                    //Database edit
                    var result = await _db.ShareCharacter(userID, shareUser, characterID, access);
                    if (result == CharacterShareResult.Success)
                    {
                        var cacheResult = await _cache.TryLoadCharacter(characterID);
                        if (cacheResult.IsSuccess && cacheResult.Value != null)
                        {
                            //Reload Permissions.
                            var permResult = await _db.GetPermissions(characterID);
                            if (permResult.IsSuccess && permResult.Value != null)
                            {
                                cacheResult.Value.Permissions = permResult.Value;

                                await _cache.TrySaveCharacter(cacheResult.Value);
                            } else
                            {
                                //Evict
                                await _cache.EvictCharacter(characterID);
                            }
                        }
                    }
                    return result;
                }
                catch
                {
                    return CharacterShareResult.UnknownError;
                }
                finally
                {
                    charLock.Release();
                }
            }
            catch
            {
                return CharacterShareResult.UnknownError;
            }
        }

        public async Task<CharacterSharePermissions> GetCharacterSharePermissions(int userId, int characterID) => await _db.GetCharacterSharePermissions(userId, characterID);

        public async Task<TryResult> UnshareCharacter(int userId, int characterID)
        {
            var charLock = _locks.GetLock(characterID);
            await charLock.WaitAsync();
            try
            {
                //Database edit
                var result = await _db.UnshareCharacter(userId, characterID);
                if (result.IsSuccess)
                {
                    var cacheResult = await _cache.TryLoadCharacter(characterID);
                    if (cacheResult.IsSuccess && cacheResult.Value != null)
                    {
                        //Reload Permissions.
                        var permResult = await _db.GetPermissions(characterID);
                        if (permResult.IsSuccess && permResult.Value != null)
                        {
                            cacheResult.Value.Permissions = permResult.Value;

                            await _cache.TrySaveCharacter(cacheResult.Value);
                        }
                        else
                        {
                            //Evict
                            await _cache.EvictCharacter(characterID);
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                return TryResult.Fail(ex.Message);
            }
            finally
            {
                charLock.Release();
            }
        }
    }
}


