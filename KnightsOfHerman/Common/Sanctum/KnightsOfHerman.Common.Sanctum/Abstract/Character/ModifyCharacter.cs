using KnightsOfHerman.Common.Abstract;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KnightsOfHerman.Common.Sanctum.Abstract.Character
{
    /// <summary>
    /// Outdates class that is only used for unit tests now
    /// </summary>
    public class ModifyCharacter
    {
        public static TryResult ModifyCharacterRecursive(object current, string path, object value, EditActions action)
        {
            string[] paths = path.Split(".", 2);
            if (paths.Count() == 2)
            {
                //use system.refletion to obtain the new current
                object next;
                if(current is IDictionary dict)
                {
                    //Get the type of key and convert the path to it.
                    Type Tkey = current.GetType().GetGenericArguments()[0];
                    var key = Convert.ChangeType(paths[0],Tkey);
                    if (!dict.Contains(key))
                    {
                        return TryResult.Fail($"Key {paths[0]} not in dictionary {current.GetType()}");
                    }
                    next = dict[key];
                } else
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
                    if(action == EditActions.Clear)
                    {
                        dict.Clear();
                        return TryResult.Success();
                    }
                    else
                    {
                        Type Tkey = current.GetType().GetGenericArguments()[0];
                        var key = Convert.ChangeType(paths[0], Tkey);

                        if (action == EditActions.Remove)
                        {
                            dict.Remove(key);
                            return TryResult.Success();
                        }
                        else
                        {
                            //This will not work for adding new. types
                            Type TValue = current.GetType().GetGenericArguments()[0];
                            object val = Convert.ChangeType(value, TValue);

                            dict[key] = val;
                            return TryResult.Success();

                        }
                    }
                } else
                {
                    //Only edits are allowed here.
                    PropertyInfo info = current.GetType().GetProperty(paths[0]);
                    if (info == null) return TryResult.Fail($"Property {paths[0]} not found in {current.GetType()}");

                    var val = Convert.ChangeType(value, info.PropertyType);
                    info.SetValue(current, val);

                    return TryResult.Success();
                }
            } 
        }
    }
}
