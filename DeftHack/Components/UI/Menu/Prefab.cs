using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;






using UnityEngine;


public static class Prefab
{
 
    [Initializer]
    public static void Initialize()
    {
        ColorUtilities.addColor(new ColorVariable("_MenuTabOff", "Menu Tab - Off", new Color32(160, 160, 160, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_MenuTabOn", "Menu Tab - On", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_MenuTabHover", "Menu Tab - Hover", new Color32(210, 210, 210, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_TextStyleOff", "Menu Labels - Off", new Color32(160, 160, 160, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_TextStyleOn", "Menu Labels - On", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_TextStyleHover", "Menu Labels - Hover", new Color32(210, 210, 210, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_HeaderStyle", "Menu Area - Header", new Color32(210, 210, 210, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_ToggleBoxBG", "Menu Toggle - Background", new Color32(71, 70, 71, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_ButtonBG", "Menu Button - Background", new Color32(130, 130, 130, byte.MaxValue), true));
    }

 
    public static void CheckStyles()
    {
        bool flag = Prefab._MenuTabStyle != null || !LoaderCoroutines.IsLoaded;
        if (!flag)
        {
            Prefab._MenuTabStyle = new GUIStyle
            {
                font = MenuComponent._TabFont,
                fontSize = 29
            };
            Prefab._HeaderStyle = new GUIStyle
            {
                font = MenuComponent._TabFont,
                fontSize = 15,
                alignment = TextAnchor.MiddleCenter
            };
            Prefab._TextStyle = new GUIStyle
            {
                font = MenuComponent._TextFont,
                fontSize = 17
            };
            Prefab._sliderStyle = new GUIStyle();
            Prefab._sliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb)
            {
                fixedWidth = 7f
            };
            Prefab._sliderVThumbStyle = new GUIStyle(GUI.skin.verticalSliderThumb)
            {
                fixedHeight = 7f
            };
            Prefab._listStyle = new GUIStyle();
            Prefab._listStyle.padding.left = (Prefab._listStyle.padding.right = (Prefab._listStyle.padding.top = (Prefab._listStyle.padding.bottom = 4)));
            Prefab._listStyle.alignment = TextAnchor.MiddleLeft;
            Prefab._listStyle.font = MenuComponent._TextFont;
            Prefab._listStyle.fontSize = 15;
            Prefab._ButtonStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft,
                font = MenuComponent._TextFont,
                fontSize = 15
            };
            Prefab._ButtonStyle.padding.left = (Prefab._ButtonStyle.padding.right = (Prefab._ButtonStyle.padding.top = (Prefab._ButtonStyle.padding.bottom = 4)));
            MenuUtilities.FixGUIStyleColor(Prefab._sliderStyle);
            MenuUtilities.FixGUIStyleColor(Prefab._MenuTabStyle);
            MenuUtilities.FixGUIStyleColor(Prefab._TextStyle);
            MenuComponent.SetGUIColors();
        }
    }
 
    public static void UpdateColors()
    {
       
            Prefab._MenuTabStyle.normal.textColor = ColorUtilities.getColor("_MenuTabOff");
            Prefab._MenuTabStyle.onNormal.textColor = ColorUtilities.getColor("_MenuTabOn");
            Prefab._MenuTabStyle.hover.textColor = ColorUtilities.getColor("_MenuTabHover");
            Prefab._MenuTabStyle.onHover.textColor = ColorUtilities.getColor("_MenuTabOn");
            Prefab._MenuTabStyle.active.textColor = ColorUtilities.getColor("_MenuTabOn");
            Prefab._MenuTabStyle.onActive.textColor = ColorUtilities.getColor("_MenuTabOn");
            Prefab._MenuTabStyle.focused.textColor = ColorUtilities.getColor("_MenuTabOff");
            Prefab._MenuTabStyle.onFocused.textColor = ColorUtilities.getColor("_MenuTabOff");
            Prefab._TextStyle.normal.textColor = ColorUtilities.getColor("_TextStyleOff");
            Prefab._TextStyle.onNormal.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._TextStyle.hover.textColor = ColorUtilities.getColor("_TextStyleHover");
            Prefab._TextStyle.onHover.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._TextStyle.active.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._TextStyle.onActive.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._TextStyle.focused.textColor = ColorUtilities.getColor("_TextStyleOff");
            Prefab._TextStyle.onFocused.textColor = ColorUtilities.getColor("_TextStyleOff");
            Prefab._HeaderStyle.normal.textColor = ColorUtilities.getColor("_HeaderStyle");
            Prefab._listStyle.normal.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._listStyle.onNormal.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._listStyle.hover.textColor = ColorUtilities.getColor("_OutlineBorderBlack");
            Prefab._ButtonStyle.normal.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._ButtonStyle.onNormal.textColor = ColorUtilities.getColor("_TextStyleOn");
            Prefab._ButtonStyle.hover.textColor = ColorUtilities.getColor("_OutlineBorderBlack");
            Prefab._ButtonStyle.onHover.textColor = ColorUtilities.getColor("_OutlineBorderBlack");
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.SetPixel(0, 0, ColorUtilities.getColor("_TextStyleHover"));
            texture2D.Apply();
            Prefab._ButtonStyle.hover.background = texture2D;
            Texture2D texture2D2 = new Texture2D(1, 1);
            texture2D2.SetPixel(0, 0, ColorUtilities.getColor("_ButtonBG"));
            texture2D2.Apply();
            Prefab._ButtonStyle.normal.background = texture2D2;
            Texture2D texture2D3 = new Texture2D(1, 1);
            texture2D3.SetPixel(0, 0, ColorUtilities.getColor("_TextStyleOn"));
            texture2D3.Apply();
            Prefab._ButtonStyle.active.background = texture2D3;
            Texture2D texture2D4 = new Texture2D(1, 1);
            texture2D4.SetPixel(0, 0, ColorUtilities.getColor("_TextStyleOn"));
            texture2D4.Apply();
            Prefab._listStyle.hover.background = texture2D4;
            Prefab._listStyle.onHover.background = texture2D4;
            Texture2D texture2D5 = new Texture2D(1, 1);
            texture2D5.SetPixel(0, 0, ColorUtilities.getColor("_ButtonBG"));
            texture2D5.Apply();
            Prefab._listStyle.normal.background = texture2D5;
            Prefab._listStyle.onNormal.background = texture2D5;
            Prefab._ToggleBoxBG = ColorUtilities.getColor("_ToggleBoxBG");
      
    }
     
    public static bool MenuTab(string text, ref bool state, int fontsize = 29)
    {
        bool result = false;
        bool flag = state;
        int fontSize = Prefab._MenuTabStyle.fontSize;
        Prefab._MenuTabStyle.fontSize = fontsize;
        bool flag2 = GUILayout.Toggle(flag, text.ToUpper(), Prefab._MenuTabStyle, new GUILayoutOption[0]);
        bool flag3 = flag != flag2;
        if (flag3)
        {
            state = !state;
            result = true;
        }
        Prefab._MenuTabStyle.fontSize = fontSize;
        return result;
    }
     
    public static bool MenuTabAbsolute(Vector2 pos, string text, ref bool state, int fontsize = 29)
    {
        bool result = false;
        bool flag = state;
        int fontSize = Prefab._MenuTabStyle.fontSize;
        Prefab._MenuTabStyle.fontSize = fontsize;
        Vector2 size = Prefab._MenuTabStyle.CalcSize(new GUIContent(text));
        Rect position = new Rect(pos, size);
        bool flag2 = GUI.Toggle(position, flag, text.ToUpper(), Prefab._MenuTabStyle);
        bool flag3 = flag != flag2;
        if (flag3)
        {
            state = !state;
            result = true;
        }
        Prefab._MenuTabStyle.fontSize = fontSize;
        return result;
    }
     
    public static void MenuArea(Rect area, string header, Action code)
    {
        Rect rect = new Rect(area.x, area.y + 5f, area.width, area.height - 5f);
        Rect rect2 = MenuUtilities.Inline(rect, 1);
        Rect position = MenuUtilities.Inline(rect2, 1);
        Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(rect2, MenuComponent._OutlineBorderLightGray, null);
        Drawing.DrawRect(position, MenuComponent._FillLightBlack, null);
        Vector2 vector = Prefab._HeaderStyle.CalcSize(new GUIContent(header));
        Drawing.DrawRect(new Rect(area.x + 18f, area.y, vector.x + 4f, vector.y), MenuComponent._FillLightBlack, null);
        GUI.Label(new Rect(area.x + 20f, area.y - 3f, vector.x, vector.y), header, Prefab._HeaderStyle);
        GUILayout.BeginArea(area);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Space(15f);
        GUILayout.BeginVertical(new GUILayoutOption[0]);
        GUILayout.Space(20f);
        try
        {
            code();
        }
        catch (Exception)
        {
            
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
     
    public static void SectionTabButton(string text, Action code, float space = 0f, int fontsize = 20)
    {
        bool flag = false;
        GUILayout.Space(space);
        bool flag2 = Prefab.MenuTab(text, ref flag, fontsize);
        if (flag2)
        {
            SectionTab.CurrentSectionTab = new SectionTab(text, code);
        }
        GUILayout.Space(space);
    }
     
    public static bool Toggle(string text, ref bool state, int fontsize = 17)
    {
        bool result = false;
        int num = 1;
        int num2 = 15;
        int fontSize = Prefab._TextStyle.fontSize;
        Prefab._TextStyle.fontSize = fontsize;
        GUILayout.Space(3f);
        Rect rect = GUILayoutUtility.GetRect(150f, 15f);
        Rect rect2 = new Rect(rect.x + num, rect.y + num, num2 - num * 2, num2 - num * 2);
        Rect position = MenuUtilities.Inline(rect2, 1);
        Drawing.DrawRect(rect2, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(position, Prefab._ToggleBoxBG, null);
        bool flag = GUI.Button(rect, GUIContent.none, Prefab._TextStyle);
        if (flag)
        {
            state = !state;
            result = true;
        }
        bool flag2 = Event.current.type == EventType.Repaint;
        if (flag2)
        {
            bool flag3 = rect.Contains(Event.current.mousePosition);
            bool isActive = flag3 && Input.GetMouseButton(0);
            Prefab._TextStyle.Draw(new Rect(rect.x + 20f, rect.y, 130f, num2), text, flag3, isActive, false, false);
        }
        Prefab._TextStyle.fontSize = fontSize;
        bool flag4 = state;
        if (flag4)
        {
            Drawing.DrawRect(position, MenuComponent._Accent2, null);
        }
        return result;
    }
     
    public static void Slider(float left, float right, ref float value, int size)
    {
        GUIStyle sliderThumbStyle = Prefab._sliderThumbStyle;
        GUIStyle sliderStyle = Prefab._sliderStyle;
        float num = (sliderThumbStyle.fixedWidth != 0f) ? sliderThumbStyle.fixedWidth : sliderThumbStyle.padding.horizontal;
        value = GUILayout.HorizontalSlider(value, left, right, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, new GUILayoutOption[]
        {
                GUILayout.Width(size)
        });
        Rect lastRect = GUILayoutUtility.GetLastRect();
        float num2 = (lastRect.width - sliderStyle.padding.horizontal - num) / (right - left);
        Rect rect = new Rect((value - left) * num2 + lastRect.x + sliderStyle.padding.left, lastRect.y + sliderStyle.padding.top, num, lastRect.height - sliderStyle.padding.vertical);
        Drawing.DrawRect(lastRect, MenuComponent._FillLightBlack, null);
        Drawing.DrawRect(new Rect(lastRect.x, lastRect.y + lastRect.height / 2f - 2f, lastRect.width, 4f), Prefab._ToggleBoxBG, null);
        Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(rect, 1), Prefab._MenuTabStyle.onNormal.textColor, null);
        GUILayout.Space(5f);
    } 
    public static float Slider(float left, float right, float value, int size)
    {
        GUIStyle sliderThumbStyle = Prefab._sliderThumbStyle;
        GUIStyle sliderStyle = Prefab._sliderStyle;
        float num = (sliderThumbStyle.fixedWidth != 0f) ? sliderThumbStyle.fixedWidth : sliderThumbStyle.padding.horizontal;
        value = GUILayout.HorizontalSlider(value, left, right, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, new GUILayoutOption[]
        {
                GUILayout.Width(size)
        });
        Rect lastRect = GUILayoutUtility.GetLastRect();
        float num2 = (lastRect.width - sliderStyle.padding.horizontal - num) / (right - left);
        Rect rect = new Rect((value - left) * num2 + lastRect.x + sliderStyle.padding.left, lastRect.y + sliderStyle.padding.top, num, lastRect.height - sliderStyle.padding.vertical);
        Drawing.DrawRect(lastRect, MenuComponent._FillLightBlack, null);
        Drawing.DrawRect(new Rect(lastRect.x, lastRect.y + lastRect.height / 2f - 2f, lastRect.width, 4f), Prefab._ToggleBoxBG, null);
        Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(rect, 1), Prefab._MenuTabStyle.onNormal.textColor, null);
        GUILayout.Space(5f);
        return value;
    }
     
    public static void VerticalSlider(Rect pos, float top, float bottom, ref float value)
    {
        GUIStyle sliderVThumbStyle = Prefab._sliderVThumbStyle;
        GUIStyle sliderStyle = Prefab._sliderStyle;
        float num = (sliderVThumbStyle.fixedHeight != 0f) ? sliderVThumbStyle.fixedHeight : sliderVThumbStyle.padding.vertical;
        value = GUI.VerticalSlider(pos, value, top, bottom, Prefab._sliderStyle, GUI.skin.verticalSliderThumb);
        Rect position = pos;
        float num2 = (position.height - sliderStyle.padding.vertical - num) / (bottom - top);
        Rect rect = new Rect(position.x + sliderStyle.padding.left, (value - top) * num2 + position.y + sliderStyle.padding.top, position.width - sliderStyle.padding.horizontal, num);
        Drawing.DrawRect(position, MenuComponent._FillLightBlack, null);
        Drawing.DrawRect(new Rect(position.x + position.width / 2f - 2f, position.y, 4f, position.height), Prefab._ToggleBoxBG, null);
        Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(rect, 1), Prefab._MenuTabStyle.onNormal.textColor, null);
    }
     
    public static void ScrollView(Rect area, string title, ref Vector2 scrollpos, Action code, int padding = 20, params GUILayoutOption[] options)
    {
        Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(area, 1), MenuComponent._OutlineBorderLightGray, null);
        Rect rect = MenuUtilities.Inline(area, 2);
        Drawing.DrawRect(rect, MenuComponent._FillLightBlack, null);
        Color textColor = Prefab._MenuTabStyle.normal.textColor;
        int fontSize = Prefab._MenuTabStyle.fontSize;
        Prefab._MenuTabStyle.normal.textColor = Prefab._MenuTabStyle.onNormal.textColor;
        Prefab._MenuTabStyle.fontSize = 15;
        Drawing.DrawRect(new Rect(rect.x, rect.y, rect.width, Prefab._MenuTabStyle.CalcSize(new GUIContent(title)).y + 2f), MenuComponent._OutlineBorderLightGray, null);
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.FlexibleSpace();
        GUILayout.Label(title, Prefab._MenuTabStyle, new GUILayoutOption[0]);
        Prefab._MenuTabStyle.normal.textColor = textColor;
        Prefab._MenuTabStyle.fontSize = fontSize;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(2f);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        scrollpos = GUILayout.BeginScrollView(scrollpos, false, true, new GUILayoutOption[0]);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Space(padding);
        GUILayout.BeginVertical(new GUILayoutOption[]
        {
                GUILayout.MinHeight(rect.height)
        });
        try
        {
            code();
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
        GUILayout.EndVertical();
        Rect lastRect = GUILayoutUtility.GetLastRect();
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        Rect lastRect2 = GUILayoutUtility.GetLastRect();
        GUILayout.Space(1f);
        GUILayout.EndHorizontal();
        GUILayout.Space(1f);
        Drawing.DrawRect(new Rect(lastRect2.x + lastRect2.width - 16f, lastRect2.y, 16f, lastRect2.height), MenuComponent._FillLightBlack, null);
        bool flag = lastRect.height - lastRect2.height > 0f;
        if (flag)
        {
            Prefab.VerticalSlider(new Rect(lastRect2.x + 4f, lastRect2.y + 8f, 12f, lastRect2.height - 14f), 0f, lastRect.height - lastRect2.height, ref scrollpos.y);
        }
        GUILayout.EndArea();
    }
     
    public static void ScrollView(Rect area, string title, ref SerializableVector2 scrollpos, Action code, int padding = 20, params GUILayoutOption[] options)
    {
        Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(area, 1), MenuComponent._OutlineBorderLightGray, null);
        Rect rect = MenuUtilities.Inline(area, 2);
        Drawing.DrawRect(rect, MenuComponent._FillLightBlack, null);
        Color textColor = Prefab._MenuTabStyle.normal.textColor;
        int fontSize = Prefab._MenuTabStyle.fontSize;
        Prefab._MenuTabStyle.normal.textColor = Prefab._MenuTabStyle.onNormal.textColor;
        Prefab._MenuTabStyle.fontSize = 15;
        Drawing.DrawRect(new Rect(rect.x, rect.y, rect.width, Prefab._MenuTabStyle.CalcSize(new GUIContent(title)).y + 2f), MenuComponent._OutlineBorderLightGray, null);
        GUILayout.BeginArea(rect);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.FlexibleSpace();
        GUILayout.Label(title, Prefab._MenuTabStyle, new GUILayoutOption[0]);
        Prefab._MenuTabStyle.normal.textColor = textColor;
        Prefab._MenuTabStyle.fontSize = fontSize;
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.Space(2f);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        scrollpos = GUILayout.BeginScrollView(scrollpos.ToVector2(), false, true, new GUILayoutOption[0]);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Space(padding);
        GUILayout.BeginVertical(new GUILayoutOption[]
        {
                GUILayout.MinHeight(rect.height)
        });
        try
        {
            code();
        }
        catch (Exception)
        {
            
        }
        GUILayout.EndVertical();
        Rect lastRect = GUILayoutUtility.GetLastRect();
        GUILayout.EndHorizontal();
        GUILayout.EndScrollView();
        Rect lastRect2 = GUILayoutUtility.GetLastRect();
        GUILayout.Space(1f);
        GUILayout.EndHorizontal();
        GUILayout.Space(1f);
        Drawing.DrawRect(new Rect(lastRect2.x + lastRect2.width - 16f, lastRect2.y, 16f, lastRect2.height), MenuComponent._FillLightBlack, null);
        bool flag = lastRect.height - lastRect2.height > 0f;
        if (flag)
        {
            Prefab.VerticalSlider(new Rect(lastRect2.x + 4f, lastRect2.y + 8f, 12f, lastRect2.height - 14f), 0f, lastRect.height - lastRect2.height, ref scrollpos.y);
        }
        GUILayout.EndArea();
    }
     
    public static bool List(float width, string identifier, GUIContent buttonContent, GUIContent[] listContent, params GUILayoutOption[] options)
    {
        Vector2 vector = Prefab._listStyle.CalcSize(buttonContent);
        List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
        list.Add(GUILayout.Height(vector.y));
        list.Add(GUILayout.Width(width));
        Rect rect = GUILayoutUtility.GetRect(width, vector.y, list.ToArray());
        DropDown dropDown = DropDown.Get(identifier);
        return Prefab.List(rect, ref dropDown.IsEnabled, ref dropDown.ListIndex, ref dropDown.ScrollView, buttonContent, listContent, "button", "box", Prefab._listStyle);
    }
     
    public static bool List(string identifier, GUIContent buttonContent, GUIContent[] listContent, params GUILayoutOption[] options)
    {
        Vector2 vector = Prefab._listStyle.CalcSize(buttonContent);
        List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
        list.Add(GUILayout.Height(vector.y));
        list.Add(GUILayout.Width(vector.x + 5f));
        Rect rect = GUILayoutUtility.GetRect(vector.x + 5f, vector.y, list.ToArray());
        DropDown dropDown = DropDown.Get(identifier);
        return Prefab.List(rect, ref dropDown.IsEnabled, ref dropDown.ListIndex, ref dropDown.ScrollView, buttonContent, listContent, "button", "box", Prefab._listStyle);
    }
     
    public static bool List(Rect position, ref bool showList, ref int listEntry, ref Vector2 scrollPos, GUIContent buttonContent, GUIContent[] listContent)
    {
        return Prefab.List(position, ref showList, ref listEntry, ref scrollPos, buttonContent, listContent, "button", "box", Prefab._listStyle);
    }
     
    public static bool List(Rect position, ref bool showList, ref int listEntry, ref Vector2 scrollPos, GUIContent buttonContent, GUIContent[] listContent, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle)
    {
        Drawing.DrawRect(position, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(position, 1), MenuComponent._OutlineBorderDarkGray, null);
        int fontSize = Prefab._TextStyle.fontSize;
        Color textColor = Prefab._TextStyle.normal.textColor;
        Prefab._TextStyle.fontSize = 15;
        Prefab._TextStyle.normal.textColor = Prefab._TextStyle.onNormal.textColor;
        Prefab._TextStyle.alignment = TextAnchor.MiddleLeft;
        GUI.Label(new Rect(position.x + position.height + 4f, position.y, position.width - position.height * 2f, position.height), buttonContent, Prefab._TextStyle);
        bool result = false;
        bool flag = Prefab.AbsButton(new Rect(position.x, position.y, position.height, position.height), "<=", new GUILayoutOption[0]);
        if (flag)
        {
            result = true;
            listEntry = Math.Max(0, listEntry - 1);
        }
        bool flag2 = Prefab.AbsButton(new Rect(position.x + position.width - position.height, position.y, position.height, position.height), "=>", new GUILayoutOption[0]);
        if (flag2)
        {
            result = true;
            listEntry = Math.Min(listContent.Length - 1, listEntry + 1);
        }
        Prefab._TextStyle.alignment = TextAnchor.UpperLeft;
        Prefab._TextStyle.fontSize = fontSize;
        Prefab._TextStyle.normal.textColor = textColor;
        return result;
    }
     
    public static bool AbsButton(Rect area, string text, params GUILayoutOption[] options)
    {
        Drawing.DrawRect(area, MenuComponent._OutlineBorderBlack, null);
        return GUI.Button(MenuUtilities.Inline(area, 1), text, Prefab._ButtonStyle);
    }
     
    public static bool Button(string text, float width, float height = 25f, params GUILayoutOption[] options)
    {
        List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
        list.Add(GUILayout.Height(height));
        list.Add(GUILayout.Width(width));
        Rect rect = GUILayoutUtility.GetRect(width, height, list.ToArray());
        return Prefab.AbsButton(rect, text, options);
    }
     
    public static bool ColorButton(float width, ColorVariable color, float height = 25f, params GUILayoutOption[] options)
    {
        List<GUILayoutOption> list = options.ToList<GUILayoutOption>();
        list.Add(GUILayout.Height(height));
        list.Add(GUILayout.Width(width));
        Rect rect = GUILayoutUtility.GetRect(width, height, list.ToArray());
        Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
        Rect rect2 = new Rect(rect.x + 4f, rect.y + 4f, rect.height - 8f, rect.height - 8f);
        bool result = GUI.Button(MenuUtilities.Inline(rect, 1), "" + color.name, Prefab._ButtonStyle);
        Drawing.DrawRect(rect2, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(MenuUtilities.Inline(rect2, 1), MenuComponent._OutlineBorderLightGray, null);
        Drawing.DrawRect(MenuUtilities.Inline(rect2, 2), color.color, null);
        return result;
    }
     
    public static string TextField(string text, string label, int width)
    {
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Label(label, Prefab._TextStyle, new GUILayoutOption[0]);
        int fontSize = Prefab._TextStyle.fontSize;
        Prefab._TextStyle.fontSize = 13;
        float y = Prefab._TextStyle.CalcSize(new GUIContent("asdf")).y;
        Rect rect = GUILayoutUtility.GetRect(width, y);
        Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height + 1f), MenuComponent._OutlineBorderLightGray, null);
        Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height), MenuComponent._FillLightBlack, null);
        text = GUI.TextField(new Rect(rect.x + 4f, rect.y + 2f, rect.width, rect.height), text, Prefab._TextStyle);
        GUILayout.FlexibleSpace();
        Prefab._TextStyle.fontSize = fontSize;
        GUILayout.EndHorizontal();
        return text;
    }
     
    public static int TextField(int text, string label, int width, int min = 0, int max = 255)
    {
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Label(label, Prefab._TextStyle, new GUILayoutOption[0]);
        int fontSize = Prefab._TextStyle.fontSize;
        Prefab._TextStyle.fontSize = 13;
        float y = Prefab._TextStyle.CalcSize(new GUIContent("asdf")).y;
        Rect rect = GUILayoutUtility.GetRect(width, y);
        Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height + 1f), MenuComponent._OutlineBorderLightGray, null);
        Drawing.DrawRect(new Rect(rect.x, rect.y + 2f, rect.width, rect.height), MenuComponent._FillLightBlack, null);
        try
        {
            int num = int.Parse(Prefab.digitsOnly.Replace(GUI.TextField(new Rect(rect.x + 4f, rect.y + 2f, rect.width, rect.height), text.ToString(), Prefab._TextStyle), ""));
            bool flag = num >= min && num <= max;
            if (flag)
            {
                text = num;
            }
        }
        catch (Exception)
        {
            
        }
        GUILayout.FlexibleSpace();
        Prefab._TextStyle.fontSize = fontSize;
        GUILayout.EndHorizontal();
        return text;
    }
     
    public static GUIStyle _MenuTabStyle;
     
    public static GUIStyle _HeaderStyle;
     
    public static GUIStyle _TextStyle;
     
    public static GUIStyle _sliderStyle;
     
    public static GUIStyle _sliderThumbStyle;
      
    public static GUIStyle _sliderVThumbStyle;
     
    public static GUIStyle _listStyle;
     
    public static GUIStyle _ButtonStyle;
     
    public static Color32 _ToggleBoxBG;
     
    private static readonly int popupListHash = "PopupList".GetHashCode();
     
    public static Regex digitsOnly = new Regex("[^\\d]");
}

