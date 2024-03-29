﻿using UnityEngine;
 
public   class Drawing
{
    
    public static void DrawRect(Rect position, Color color, GUIContent content = null)
    {
        Color backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUI.Box(position, content ?? GUIContent.none, Drawing.textureStyle);
        GUI.backgroundColor = backgroundColor;
    }
     
    public static void LayoutBox(Color color, GUIContent content = null)
    {
        Color backgroundColor = GUI.backgroundColor;
        GUI.backgroundColor = color;
        GUILayout.Box(content ?? GUIContent.none, Drawing.textureStyle, new GUILayoutOption[0]);
        GUI.backgroundColor = backgroundColor;
    }

    public static   Texture2D backgroundTexture = Texture2D.whiteTexture;
   
   public static   GUIStyle textureStyle = new GUIStyle
    {
        normal = new GUIStyleState
        {
            background = Drawing.backgroundTexture
        }
    };
}