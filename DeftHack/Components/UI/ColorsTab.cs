using System;
using System.Collections.Generic;
using System.Linq;



using UnityEngine;

 
public static class ColorsTab
{ 
    public static Color LastColorPreview1
    {
        get => ColorsTab.LastColorPreview;
        set => ColorsTab.LastColorPreview = value;
    }

    public static void Tab()
    {
        bool flag = ColorOptions.selectedOption == null;
        if (flag)
        {
            ColorOptions.previewselected = ColorOptions.preview;
        }
        Prefab.ScrollView(new Rect(0f, 0f, 250f, 436f), "Цвета", ref ColorsTab.ColorScroll, delegate ()
        {
            GUILayout.Space(10f);
            List<KeyValuePair<string, ColorVariable>> list = ColorOptions.ColorDict.ToList<KeyValuePair<string, ColorVariable>>();
            list.Sort((KeyValuePair<string, ColorVariable> pair1, KeyValuePair<string, ColorVariable> pair2) => string.Compare(pair1.Value.name, pair2.Value.name, StringComparison.Ordinal));
            for (int i = 0; i < list.Count; i++)
            {
                ColorVariable color = ColorUtilities.getColor(list[i].Value.identity);
                bool flag2 = Prefab.ColorButton(205f, color, 25f, new GUILayoutOption[0]);
                if (flag2)
                {
                    ColorOptions.selectedOption = color.identity;
                    ColorOptions.previewselected = new ColorVariable(color);
                    ColorsTab.LastColorPreview1 = color.color;
                }
                GUILayout.Space(3f);
            }
            bool flag3 = Prefab.Button("Восстановить по умолчанию", 205f, 25f, new GUILayoutOption[0]);
            if (flag3)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    ColorVariable color2 = ColorUtilities.getColor(list[j].Value.identity);
                    color2.color = color2.origColor;
                    MenuComponent.SetGUIColors();
                    ColorOptions.selectedOption = null;
                    ColorsTab.LastColorPreview1 = ColorOptions.preview.color;
                }
            }
            GUILayout.Space(10f);
        }, 20, new GUILayoutOption[0]);
        Rect previewRect = new Rect(255f, 0f, 211f, 120f);
        Prefab.MenuArea(previewRect, "Предпросмотр", delegate
        {
            Rect rect;
            rect = new Rect(5f, 20f, previewRect.width - 10f, previewRect.height - 25f);
            Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
            Drawing.DrawRect(MenuUtilities.Inline(rect, 1), MenuComponent._OutlineBorderLightGray, null);
            Rect rect2 = MenuUtilities.Inline(rect, 2);
            Drawing.DrawRect(new Rect(rect2.x, rect2.y, rect2.width / 2f, rect2.height), ColorsTab.LastColorPreview1, null);
            Drawing.DrawRect(new Rect(rect2.x + rect2.width / 2f, rect2.y, rect2.width / 2f, rect2.height), ColorOptions.previewselected, null);
        });
        Prefab.MenuArea(new Rect(previewRect.x, previewRect.y + previewRect.height + 5f, previewRect.width, 436f - previewRect.height - 5f), ColorOptions.previewselected.name, delegate
        {
            GUILayout.BeginArea(new Rect(10f, 20f, previewRect.width - 10f, 205f));
            ColorOptions.previewselected.color.r = (byte)Prefab.TextField(ColorOptions.previewselected.color.r, "R: ", 30, 0, 255);
            ColorOptions.previewselected.color.r = (byte)Mathf.Round(Prefab.Slider(0f, 255f, ColorOptions.previewselected.color.r, 185));
            GUILayout.FlexibleSpace();
            ColorOptions.previewselected.color.g = (byte)Prefab.TextField(ColorOptions.previewselected.color.g, "G: ", 30, 0, 255);
            ColorOptions.previewselected.color.g = (byte)Mathf.Round(Prefab.Slider(0f, 255f, ColorOptions.previewselected.color.g, 185));
            GUILayout.FlexibleSpace();
            ColorOptions.previewselected.color.b = (byte)Prefab.TextField(ColorOptions.previewselected.color.b, "B: ", 30, 0, 255);
            ColorOptions.previewselected.color.b = (byte)Mathf.Round(Prefab.Slider(0f, 255f, ColorOptions.previewselected.color.b, 185));
            GUILayout.FlexibleSpace();
            bool flag2 = !ColorOptions.previewselected.disableAlpha;
            if (flag2)
            {
                ColorOptions.previewselected.color.a = (byte)Prefab.TextField(ColorOptions.previewselected.color.a, "A: ", 30, 0, 255);
                ColorOptions.previewselected.color.a = (byte)Mathf.Round(Prefab.Slider(0f, 255f, ColorOptions.previewselected.color.a, 185));
            }
            else
            {
                Prefab.TextField(ColorOptions.previewselected.color.a, "A: ", 30, 0, 255);
                Prefab.Slider(0f, 255f, ColorOptions.previewselected.color.a, 185);
            }
            GUILayout.Space(100f);
            GUILayout.EndArea();
            GUILayout.Space(160f);
            GUILayout.FlexibleSpace();
            bool flag3 = Prefab.Button("Отменить", 180f, 25f, new GUILayoutOption[0]);
            if (flag3)
            {
                ColorOptions.selectedOption = null;
                ColorsTab.LastColorPreview1 = ColorOptions.preview.color;
            }
            GUILayout.Space(3f);
            bool flag4 = Prefab.Button("Восстановить", 180f, 25f, new GUILayoutOption[0]);
            if (flag4)
            {
                ColorUtilities.setColor(ColorOptions.previewselected.identity, ColorOptions.previewselected.origColor);
                ColorOptions.previewselected.color = ColorOptions.previewselected.origColor;
                MenuComponent.SetGUIColors();
            }
            GUILayout.Space(3f);
            bool flag5 = Prefab.Button("Применить", 180f, 25f, new GUILayoutOption[0]);
            if (flag5)
            {

                ColorUtilities.setColor(ColorOptions.previewselected.identity, ColorOptions.previewselected.color);
                MenuComponent.SetGUIColors();

                ColorsTab.LastColorPreview1 = ColorOptions.previewselected.color;
            }
            GUILayout.Space(30f);
        });
    }
     
    public static Vector2 ColorScroll;
     
    public static Color LastColorPreview = ColorOptions.preview.color;
}

