using KnightsOfHerman.Common.Sanctum.Abstract.Character.Items;
using KnightsOfHerman.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Sanctum.Character.Types
{
    /// <summary>
    /// Character's Equipped Armor Representation
    /// </summary>
    public class CharacterEquipedArmor
    {
        public CharacterEquipedArmor()
        {
            ArmorSlots = new();
            foreach (ArmorSlot slot in Enum.GetValues(typeof(ArmorSlot)))
            {
                ArmorSlots.Add(slot, new ArmorSlotLayers());
            }
        }

        /// <summary>
        /// Dictionary of Armor Slots
        /// </summary>
        public Dictionary<ArmorSlot, ArmorSlotLayers> ArmorSlots { get; set; }

        /// <summary>
        /// Attempts to equip armor, fails if a slot is already taken
        /// </summary>
        /// <param name="armor"></param>
        /// <returns></returns>
        public TryResult TryEquipArmor(CharacterArmorCO armor)
        {
            //Check if all slots are available
            foreach (var slot in ArmorSlots.Keys)
            {
                if ((armor.Slot & slot) == slot && !ArmorSlots[slot].IsLayerEmpty(armor.Layer))
                {
                    if (armor.ItemID == ArmorSlots[slot].Layers[armor.Layer]?.ItemID)
                    {
                        //This armor piece is already equipped so just return success.
                        return TryResult.Success();
                    }
                    armor.Equipped = false;
                    return TryResult.Fail("A slot of this armor type is already equipped.");
                }
            }

            foreach (var slot in ArmorSlots.Keys)
            {
                if ((armor.Slot & slot) == slot)
                {
                    ArmorSlots[slot].AddOrChangeArmor(armor);
                }
            }

            return TryResult.Success();
        }

        /// <summary>
        /// Unequips the armor
        /// </summary>
        /// <param name="armor"></param>
        public void UnEquipArmor(CharacterArmorCO armor)
        {
            UnEquipArmor(armor.Slot, armor.Layer);
        }

        public void UnEquipArmor(ArmorSlot slot, ArmorLayer layer)
        {
            foreach (var key in ArmorSlots.Keys)
            {
                if ((slot & key) == key)
                {
                    ArmorSlots[key].RemoveArmor(layer);
                }
            }
        }

        public class ArmorSlotLayers
        {
            public Dictionary<ArmorLayer, CharacterArmorCO?> Layers { get; set; }

            /// <summary>
            /// Armor on the Light layer
            /// </summary>
            public CharacterArmorCO? Light => Layers[ArmorLayer.Light];

            /// <summary>
            /// Armor on the Medium Layer
            /// </summary>
            public CharacterArmorCO? Medium => Layers[ArmorLayer.Medium];

            /// <summary>
            /// Armor on the Heavy Layer
            /// </summary>
            public CharacterArmorCO? Heavy => Layers[ArmorLayer.Heavy];

            /// <summary>
            /// Total hindrance of the armors
            /// </summary>
            public int Hindrance { get; private set; }

            /// <summary>
            /// Total Bludgeoning on this slot
            /// </summary>
            public int Bludgeoning { get; private set; }

            /// <summary>
            /// Total Slashing on this slot
            /// </summary>
            public int Slashing { get; private set; }

            /// <summary>
            /// Total piercing on this slot
            /// </summary>
            public int Piercing { get; private set; }

            /*
            public List<Effect> Effects
            {
                get
                {
                    if (Heavy != null)
                    {
                        return Heavy.Effects;
                    }
                    else if (Medium != null)
                    {
                        return Medium.Effects;
                    }
                    else if (Light != null)
                    {
                        return Light.Effects;
                    }
                    else return new();
                }
            }*/

            public ArmorSlotLayers()
            {
                Layers = new()
            {
                { ArmorLayer.Light, null },
                { ArmorLayer.Medium, null },
                { ArmorLayer.Heavy, null },
            };
            }

            /// <summary>
            /// Unequips the armor on a layer
            /// </summary>
            /// <param name="layer"></param>
            void SetLayerToUnequipped(ArmorLayer layer)
            {
                if (Layers.ContainsKey(layer) && Layers[layer] != null)
                {
                    Layers[layer].OnTrackedModification -= (x, y) => Recalculate();
                    Layers[layer].Equipped = false;
                }
            }

            /// <summary>
            /// Changes the current armor to the given one
            /// </summary>
            /// <param name="armor"></param>
            public void AddOrChangeArmor(CharacterArmorCO armor)
            {
                SetLayerToUnequipped(armor.Layer);
                Layers[armor.Layer] = armor;
                Layers[armor.Layer].OnTrackedModification += (x, y) => Recalculate();
                armor.Equipped = true;
                Recalculate();
            }
            /// <summary>
            /// Removes the armor from the layer
            /// </summary>
            public void RemoveArmor(CharacterArmorCO armor)
            {
                SetLayerToUnequipped(armor.Layer);
                Layers[armor.Layer] = null;
                Recalculate();
            }

            /// <summary>
            /// Removes the armor from the layer
            /// </summary>
            /// <param name="layer"></param>
            public void RemoveArmor(ArmorLayer layer)
            {
                SetLayerToUnequipped(layer);
                Layers[layer] = null;
                Recalculate();
            }

            /// <summary>
            /// Checks if the Layer is empty
            /// </summary>
            /// <param name="layer"></param>
            /// <returns></returns>
            public bool IsLayerEmpty(ArmorLayer layer)
            {
                bool key = Layers.ContainsKey(layer);

                if (!key) return true;

                var obj = Layers[layer];
                bool isnull = obj == null;
                
                return isnull;
            }

            /// <summary>
            /// Recalculates the armor totals
            /// </summary>
            private void Recalculate()
            {
                Bludgeoning = LayeringAlgorithm
                    (
                        Layers[ArmorLayer.Heavy]?.Bludgeoning ?? 0,
                        Layers[ArmorLayer.Medium]?.Bludgeoning ?? 0,
                        Layers[ArmorLayer.Light]?.Bludgeoning ?? 0
                    );
                Piercing = LayeringAlgorithm
                    (
                        Layers[ArmorLayer.Heavy]?.Piercing ?? 0,
                        Layers[ArmorLayer.Medium]?.Piercing ?? 0,
                        Layers[ArmorLayer.Light]?.Piercing ?? 0
                    );
                Slashing = LayeringAlgorithm
                    (
                        Layers[ArmorLayer.Heavy]?.Slashing ?? 0,
                        Layers[ArmorLayer.Medium]?.Slashing ?? 0,
                        Layers[ArmorLayer.Light]?.Slashing ?? 0
                    );
            }

            /// <summary>
            /// Implements the Layring Armor algorithm from the TTRPG
            /// </summary>
            /// <param name="heavy">0 if not wearing</param>
            /// <param name="medium">0 if not wearing</param>
            /// <param name="light">0 if not wearing</param>
            /// <returns>Total Soak Value of the type</returns>
            private int LayeringAlgorithm(int heavy, int medium, int light)
            {
                int total = light;
                if (medium > 0)
                {
                    total = medium + total / 2;
                }
                if (heavy > 0)
                {
                    total = heavy + total / 2;
                }
                return total;
            }
        }
    }
}