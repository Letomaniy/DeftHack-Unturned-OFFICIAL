using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

 
public static class ItemUtilities
{ 
    public static bool Whitelisted(ItemAsset asset, ItemOptionList OptionList)
    {
        bool flag = OptionList.ItemfilterCustom && OptionList.AddedItems.Contains(asset.id);
        bool result;
        if (flag)
        {
            result = true;
        }
        else
        {
            bool flag2 = OptionList.ItemfilterGun && asset is ItemGunAsset;
            if (flag2)
            {
                result = true;
            }
            else
            {
                if (OptionList.ItemfilterGunMeel && asset is ItemMeleeAsset)
                {
                    result = true;
                }
                else
                {
                    bool flag3 = OptionList.ItemfilterAmmo && asset is ItemMagazineAsset;
                    if (flag3)
                    {
                        result = true;
                    }
                    else
                    {
                        bool flag4 = OptionList.ItemfilterMedical && asset is ItemMedicalAsset;
                        if (flag4)
                        {
                            result = true;
                        }
                        else
                        {
                            bool flag5 = OptionList.ItemfilterFoodAndWater && (asset is ItemFoodAsset || asset is ItemWaterAsset);
                            if (flag5)
                            {
                                result = true;
                            }
                            else
                            {
                                bool flag6 = OptionList.ItemfilterBackpack && asset is ItemBackpackAsset;
                                if (flag6)
                                {
                                    result = true;
                                }
                                else
                                {
                                    bool flag7 = OptionList.ItemfilterCharges && asset is ItemChargeAsset;
                                    if (flag7)
                                    {
                                        result = true;
                                    }
                                    else
                                    {
                                        bool flag8 = OptionList.ItemfilterFuel && asset is ItemFuelAsset;
                                        if (flag8)
                                        {
                                            result = true;
                                        }
                                        else
                                        {
                                            bool flag9 = OptionList.ItemfilterClothing && asset is ItemClothingAsset;
                                            result = flag9;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return result;
    }
     
    public static void DrawItemButton(ItemAsset asset, HashSet<ushort> AddedItems)
    {
        string text = asset.itemName;
        bool flag = asset.itemName.Length > 60;
        if (flag)
        {
            text = asset.itemName.Substring(0, 60) + "..";
        }
        bool flag2 = Prefab.Button(text, 490f, 25f, new GUILayoutOption[0]);
        if (flag2)
        {
            bool flag3 = AddedItems.Contains(asset.id);
            if (flag3)
            {
                AddedItems.Remove(asset.id);
            }
            else
            {
                AddedItems.Add(asset.id);
            }
        }
        GUILayout.Space(3f);
    }
     
    public static void DrawFilterTab(ItemOptionList OptionList)
    {
        System.Action two = null;
        System.Action tri = null;
        System.Action one = null;
        Prefab.SectionTabButton("ФИЛЬТР ПРЕДМЕТОВ", delegate
        {
            Prefab.Toggle("Оружие", ref OptionList.ItemfilterGun, 17);
            Prefab.Toggle("Оружие ближнего боя", ref OptionList.ItemfilterGunMeel, 17);
            Prefab.Toggle("Боеприпасы", ref OptionList.ItemfilterAmmo, 17);
            Prefab.Toggle("Медикаменты", ref OptionList.ItemfilterMedical, 17);
            Prefab.Toggle("Рюкзаки", ref OptionList.ItemfilterBackpack, 17);
            Prefab.Toggle("Charges", ref OptionList.ItemfilterCharges, 17);
            Prefab.Toggle("Топливо", ref OptionList.ItemfilterFuel, 17);
            Prefab.Toggle("Одежда", ref OptionList.ItemfilterClothing, 17);
            Prefab.Toggle("Провизия", ref OptionList.ItemfilterFoodAndWater, 17);
            Prefab.Toggle("Настройка фильтра", ref OptionList.ItemfilterCustom, 17);
            bool itemfilterCustom = OptionList.ItemfilterCustom;
            if (itemfilterCustom)
            {
                GUILayout.Space(5f);
                string text = "Кастомизация фильтра";
                System.Action code;
                if ((code = one) == null)
                {
                    code = (one = delegate ()
                    {
                        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                        GUILayout.Space(55f);
                        OptionList.searchstring = Prefab.TextField(OptionList.searchstring, "Поиск:", 200);
                        GUILayout.Space(5f);
                        bool flag = Prefab.Button("Обновить", 276f, 25f, new GUILayoutOption[0]);
                        if (flag)
                        {
                            ItemsComponent.RefreshItems();
                        }
                        GUILayout.FlexibleSpace();
                        GUILayout.EndHorizontal();
                        Rect area = new Rect(70f, 50f, 540f, 190f);
                        string title = "Добавить";
                        ItemOptionList optionList = OptionList;
                        System.Action code2;
                        if ((code2 = two) == null)
                        {
                            code2 = (two = delegate ()
                            {
                                GUILayout.Space(5f);
                                for (int i = 0; i < ItemsComponent.items.Count; i++)
                                {
                                    ItemAsset itemAsset = ItemsComponent.items[i];
                                    bool flag2 = false;
                                    bool flag3 = itemAsset.itemName.ToLower().Contains(OptionList.searchstring.ToLower());
                                    if (flag3)
                                    {
                                        flag2 = true;
                                    }
                                    bool flag4 = OptionList.searchstring.Length < 2;
                                    if (flag4)
                                    {
                                        flag2 = false;
                                    }
                                    bool flag5 = OptionList.AddedItems.Contains(itemAsset.id);
                                    if (flag5)
                                    {
                                        flag2 = false;
                                    }
                                    bool flag6 = flag2;
                                    if (flag6)
                                    {
                                        ItemUtilities.DrawItemButton(itemAsset, OptionList.AddedItems);
                                    }
                                }
                                GUILayout.Space(2f);
                            });
                        }
                        Prefab.ScrollView(area, title, ref optionList.additemscroll, code2, 20, new GUILayoutOption[0]);
                        Rect area2 = new Rect(70f, 245f, 540f, 191f);
                        string title2 = "Удалить";
                        ItemOptionList optionList2 = OptionList;
                        System.Action code3;
                        if ((code3 = tri) == null)
                        {
                            code3 = (tri = delegate ()
                            {
                                GUILayout.Space(5f);
                                for (int i = 0; i < ItemsComponent.items.Count; i++)
                                {
                                    ItemAsset itemAsset = ItemsComponent.items[i];
                                    bool flag2 = false;
                                    bool flag3 = itemAsset.itemName.ToLower().Contains(OptionList.searchstring.ToLower());
                                    if (flag3)
                                    {
                                        flag2 = true;
                                    }
                                    bool flag4 = !OptionList.AddedItems.Contains(itemAsset.id);
                                    if (flag4)
                                    {
                                        flag2 = false;
                                    }
                                    bool flag5 = flag2;
                                    if (flag5)
                                    {
                                        ItemUtilities.DrawItemButton(itemAsset, OptionList.AddedItems);
                                    }
                                }
                                GUILayout.Space(2f);
                            });
                        }
                        Prefab.ScrollView(area2, title2, ref optionList2.removeitemscroll, code3, 20, new GUILayoutOption[0]);
                    });
                }
                Prefab.SectionTabButton(text, code, 0f, 20);
            }
        }, 0f, 20);
    }
}

