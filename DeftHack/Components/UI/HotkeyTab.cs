using System.Collections.Generic;
using System.Linq;



using UnityEngine;

 
public static class HotkeyTab
{
    
    public static void Tab()
    {
        Prefab.ScrollView(new Rect(0f, 0f, 466f, 400f), "Горячие клавиши", ref HotkeyTab.HotkeyScroll, delegate ()
        {
            foreach (KeyValuePair<string, Dictionary<string, Hotkey>> keyValuePair in HotkeyOptions.HotkeyDict)
            {
                bool isFirst = HotkeyTab.IsFirst;
                if (isFirst)
                {
                    HotkeyTab.IsFirst = false;
                    HotkeyTab.DrawSpacer(keyValuePair.Key, true);
                }
                else
                {
                    HotkeyTab.DrawSpacer(keyValuePair.Key, false);
                }
                foreach (KeyValuePair<string, Hotkey> keyValuePair2 in keyValuePair.Value)
                {
                    HotkeyTab.DrawButton(keyValuePair2.Value.Name, keyValuePair2.Key);
                }
            }
        }, 20, new GUILayoutOption[0]);
    }

   
    public static void DrawSpacer(string Text, bool First)
    {
        bool flag = !First;
        if (flag)
        {
            GUILayout.Space(10f);
        }
        Prefab._TextStyle.fontStyle = FontStyle.Bold;
        GUILayout.Label(Text, Prefab._TextStyle, new GUILayoutOption[0]);
        Prefab._TextStyle.fontStyle = FontStyle.Normal;
        GUILayout.Space(8f);
    } 
    public static void DrawButton(string Option, string Identifier)
    {
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Label(Option, Prefab._TextStyle, new GUILayoutOption[0]);
        bool flag = HotkeyTab.ClickedOption == Identifier;
        if (flag)
        {
            bool flag2 = Prefab.Button("Убрать", 75f, 25f, new GUILayoutOption[0]);
            if (flag2)
            {
                HotkeyComponent.Clear();
                HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = new KeyCode[0];
                HotkeyTab.ClickedOption = "";
            }
            bool flag3 = !HotkeyComponent.StopKeys;
            if (flag3)
            {
                bool flag4 = HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0;
                string text;
                if (flag4)
                {
                    text = string.Join(" + ", (from k in HotkeyOptions.UnorganizedHotkeys[Identifier].Keys
                                               select k.ToString()).ToArray<string>());
                }
                else
                {
                    text = "Не назначена";
                }
                Prefab.Button(text, 200f, 25f, new GUILayoutOption[0]);
            }
            else
            {
                HotkeyOptions.UnorganizedHotkeys[Identifier].Keys = HotkeyComponent.CurrentKeys.ToArray();
                HotkeyComponent.Clear();
                Prefab.Button(string.Join(" + ", (from k in HotkeyOptions.UnorganizedHotkeys[Identifier].Keys
                                                  select k.ToString()).ToArray<string>()), 200f, 25f, new GUILayoutOption[0]);
                HotkeyTab.ClickedOption = "";
            }
        }
        else
        {
            bool flag5 = HotkeyOptions.UnorganizedHotkeys[Identifier].Keys.Length != 0;
            string text2;
            if (flag5)
            {
                text2 = string.Join(" + ", (from k in HotkeyOptions.UnorganizedHotkeys[Identifier].Keys
                                            select k.ToString()).ToArray<string>());
            }
            else
            {
                text2 = "Не назначена";
            }
            bool flag6 = Prefab.Button(text2, 200f, 25f, new GUILayoutOption[0]);
            if (flag6)
            {
                HotkeyComponent.Clear();
                HotkeyTab.ClickedOption = Identifier;
                HotkeyComponent.NeedsKeys = true;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(2f);
    }
     
    public static Vector2 HotkeyScroll; 
    public static string ClickedOption; 
    public static bool IsFirst = true;
}

