using SDG.Provider;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

 
public static class SkinsUtilities
{
    
    public static HumanClothes CharacterClothes => OptimizationVariables.MainPlayer.clothing.characterClothes;
     
    public static HumanClothes FirstClothes => OptimizationVariables.MainPlayer.clothing.firstClothes;
     
    public static HumanClothes ThirdClothes => OptimizationVariables.MainPlayer.clothing.thirdClothes;
     
    public static void Apply(Skin skin, SkinType skinType)
    {
        bool flag = skinType == SkinType.Weapons;
        if (flag)
        {
            Dictionary<ushort, int> itemSkins = OptimizationVariables.MainPlayer.channel.owner.itemSkins;
            bool flag2 = itemSkins == null;
            if (!flag2)
            {
                ushort inventoryItemID = Provider.provider.economyService.getInventoryItemID(skin.ID);
                SkinOptions.SkinConfig.WeaponSkins.Clear();
                bool flag3 = itemSkins.TryGetValue(inventoryItemID, out int num);
                if (flag3)
                {
                    itemSkins[inventoryItemID] = skin.ID;
                }
                else
                {
                    itemSkins.Add(inventoryItemID, skin.ID);
                }
                OptimizationVariables.MainPlayer.equipment.applySkinVisual();
                OptimizationVariables.MainPlayer.equipment.applyMythicVisual();
                foreach (KeyValuePair<ushort, int> keyValuePair in itemSkins)
                {
                    SkinOptions.SkinConfig.WeaponSkins.Add(new WeaponSave(keyValuePair.Key, keyValuePair.Value));
                }
            }
        }
        else
        {
            SkinsUtilities.ApplyClothing(skin, skinType);
        }
    }
     
    public static void ApplyClothing(Skin skin, SkinType type)
    {
        switch (type)
        {
            case SkinType.Shirts:
                SkinsUtilities.CharacterClothes.visualShirt = skin.ID;
                SkinsUtilities.FirstClothes.visualShirt = skin.ID;
                SkinsUtilities.ThirdClothes.visualShirt = skin.ID;
                SkinOptions.SkinConfig.ShirtID = skin.ID;
                break;
            case SkinType.Pants:
                SkinsUtilities.CharacterClothes.visualPants = skin.ID;
                SkinsUtilities.FirstClothes.visualPants = skin.ID;
                SkinsUtilities.ThirdClothes.visualPants = skin.ID;
                SkinOptions.SkinConfig.PantsID = skin.ID;
                break;
            case SkinType.Backpacks:
                SkinsUtilities.CharacterClothes.visualBackpack = skin.ID;
                SkinsUtilities.FirstClothes.visualBackpack = skin.ID;
                SkinsUtilities.ThirdClothes.visualBackpack = skin.ID;
                SkinOptions.SkinConfig.BackpackID = skin.ID;
                break;
            case SkinType.Vests:
                SkinsUtilities.CharacterClothes.visualVest = skin.ID;
                SkinsUtilities.FirstClothes.visualVest = skin.ID;
                SkinsUtilities.ThirdClothes.visualVest = skin.ID;
                SkinOptions.SkinConfig.VestID = skin.ID;
                break;
            case SkinType.Hats:
                SkinsUtilities.CharacterClothes.visualHat = skin.ID;
                SkinsUtilities.FirstClothes.visualHat = skin.ID;
                SkinsUtilities.ThirdClothes.visualHat = skin.ID;
                SkinOptions.SkinConfig.HatID = skin.ID;
                break;
            case SkinType.Masks:
                SkinsUtilities.CharacterClothes.visualMask = skin.ID;
                SkinsUtilities.FirstClothes.visualMask = skin.ID;
                SkinsUtilities.ThirdClothes.visualMask = skin.ID;
                SkinOptions.SkinConfig.MaskID = skin.ID;
                break;
            case SkinType.Glasses:
                SkinsUtilities.CharacterClothes.visualGlasses = skin.ID;
                SkinsUtilities.FirstClothes.visualGlasses = skin.ID;
                SkinsUtilities.ThirdClothes.visualGlasses = skin.ID;
                SkinOptions.SkinConfig.GlassesID = skin.ID;
                break;
        }
        SkinsUtilities.CharacterClothes.apply();
        SkinsUtilities.FirstClothes.apply();
        SkinsUtilities.ThirdClothes.apply();
    }
     
    public static void ApplyFromConfig()
    {
        Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
        foreach (WeaponSave weaponSave in SkinOptions.SkinConfig.WeaponSkins)
        {
            dictionary[weaponSave.WeaponID] = weaponSave.SkinID;
        }
        bool flag = OptimizationVariables.MainPlayer == null;
        if (!flag)
        {
            OptimizationVariables.MainPlayer.channel.owner.itemSkins = dictionary;
            bool flag2 = SkinOptions.SkinConfig.ShirtID != 0;
            if (flag2)
            {
                SkinsUtilities.CharacterClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
                SkinsUtilities.FirstClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
                SkinsUtilities.ThirdClothes.visualShirt = SkinOptions.SkinConfig.ShirtID;
            }
            bool flag3 = SkinOptions.SkinConfig.PantsID != 0;
            if (flag3)
            {
                SkinsUtilities.CharacterClothes.visualPants = SkinOptions.SkinConfig.PantsID;
                SkinsUtilities.FirstClothes.visualPants = SkinOptions.SkinConfig.PantsID;
                SkinsUtilities.ThirdClothes.visualPants = SkinOptions.SkinConfig.PantsID;
            }
            bool flag4 = SkinOptions.SkinConfig.BackpackID != 0;
            if (flag4)
            {
                SkinsUtilities.CharacterClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
                SkinsUtilities.FirstClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
                SkinsUtilities.ThirdClothes.visualBackpack = SkinOptions.SkinConfig.BackpackID;
            }
            bool flag5 = SkinOptions.SkinConfig.VestID != 0;
            if (flag5)
            {
                SkinsUtilities.CharacterClothes.visualVest = SkinOptions.SkinConfig.VestID;
                SkinsUtilities.FirstClothes.visualVest = SkinOptions.SkinConfig.VestID;
                SkinsUtilities.ThirdClothes.visualVest = SkinOptions.SkinConfig.VestID;
            }
            bool flag6 = SkinOptions.SkinConfig.HatID != 0;
            if (flag6)
            {
                SkinsUtilities.CharacterClothes.visualHat = SkinOptions.SkinConfig.HatID;
                SkinsUtilities.FirstClothes.visualHat = SkinOptions.SkinConfig.HatID;
                SkinsUtilities.ThirdClothes.visualHat = SkinOptions.SkinConfig.HatID;
            }
            bool flag7 = SkinOptions.SkinConfig.MaskID != 0;
            if (flag7)
            {
                SkinsUtilities.CharacterClothes.visualMask = SkinOptions.SkinConfig.MaskID;
                SkinsUtilities.FirstClothes.visualMask = SkinOptions.SkinConfig.MaskID;
                SkinsUtilities.ThirdClothes.visualMask = SkinOptions.SkinConfig.MaskID;
            }
            bool flag8 = SkinOptions.SkinConfig.GlassesID != 0;
            if (flag8)
            {
                SkinsUtilities.CharacterClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
                SkinsUtilities.FirstClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
                SkinsUtilities.ThirdClothes.visualGlasses = SkinOptions.SkinConfig.GlassesID;
            }
            SkinsUtilities.CharacterClothes.apply();
            SkinsUtilities.FirstClothes.apply();
            SkinsUtilities.ThirdClothes.apply();
        }
    }
     
    public static void DrawSkins(SkinOptionList OptionList)
    {
        System.Action one = null;
        Prefab.SectionTabButton(OptionList.Type.ToString(), delegate
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Space(60f);
            SkinsUtilities.SearchString = Prefab.TextField(SkinsUtilities.SearchString, "Search:", 480);
            GUILayout.EndHorizontal();
            Rect area = new Rect(70f, 40f, 540f, 395f);
            string title = OptionList.Type.ToString();
            System.Action code;
            if ((code = one) == null)
            {
                code = (one = delegate ()
                {
                    foreach (Skin skin in OptionList.Skins)
                    {
                        bool flag = skin.Name.ToLower().Contains(SkinsUtilities.SearchString.ToLower());
                        bool flag2 = flag;
                        if (flag2)
                        {
                            bool flag3 = Prefab.Button(skin.Name, 495f, 25f, new GUILayoutOption[0]);
                            if (flag3)
                            {
                                SkinsUtilities.Apply(skin, OptionList.Type);
                            }
                        }
                    }
                });
            }
            Prefab.ScrollView(area, title, ref SkinsUtilities.ScrollPos, code, 20, new GUILayoutOption[0]);
        }, 0f, 20);
    }
     
    public static void RefreshEconInfo()
    {
        bool flag = SkinOptions.SkinWeapons.Skins.Count > 5;
        if (!flag)
        {
            foreach (UnturnedEconInfo unturnedEconInfo in TempSteamworksEconomy.econInfo)
            {
                bool flag2 = unturnedEconInfo.type.Contains("Skin");
                if (flag2)
                {
                    SkinOptions.SkinWeapons.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag3 = unturnedEconInfo.type.Contains("Shirt");
                if (flag3)
                {
                    SkinOptions.SkinClothesShirts.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag4 = unturnedEconInfo.type.Contains("Pants");
                if (flag4)
                {
                    SkinOptions.SkinClothesPants.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag5 = unturnedEconInfo.type.Contains("Backpack");
                if (flag5)
                {
                    SkinOptions.SkinClothesBackpack.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag6 = unturnedEconInfo.type.Contains("Vest");
                if (flag6)
                {
                    SkinOptions.SkinClothesVest.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag7 = unturnedEconInfo.type.Contains("Hat");
                if (flag7)
                {
                    SkinOptions.SkinClothesHats.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag8 = unturnedEconInfo.type.Contains("Mask");
                if (flag8)
                {
                    SkinOptions.SkinClothesMask.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
                bool flag9 = unturnedEconInfo.type.Contains("Glass");
                if (flag9)
                {
                    SkinOptions.SkinClothesGlasses.Skins.Add(new Skin(unturnedEconInfo.name, unturnedEconInfo.itemdefid));
                }
            }
        }
    }
     
    public static Vector2 ScrollPos; 
    public static string SearchString = "";
}

