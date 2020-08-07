using UnityEngine;

 
public class MenuUtilities
{
 
    public MenuUtilities()
    {
        MenuUtilities.TexClear.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
        MenuUtilities.TexClear.Apply();
    }

     
    public static void FixGUIStyleColor(GUIStyle style)
    {
        style.normal.background = MenuUtilities.TexClear;
        style.onNormal.background = MenuUtilities.TexClear;
        style.hover.background = MenuUtilities.TexClear;
        style.onHover.background = MenuUtilities.TexClear;
        style.active.background = MenuUtilities.TexClear;
        style.onActive.background = MenuUtilities.TexClear;
        style.focused.background = MenuUtilities.TexClear;
        style.onFocused.background = MenuUtilities.TexClear;
    }
     
    public static Rect Inline(Rect rect, int border = 1)
    {
        Rect result;
        result = new Rect(rect.x + border, rect.y + border, rect.width - border * 2, rect.height - border * 2);
        return result;
    }
     
    public static Rect AbsRect(Vector2 pos1, Vector2 pos2)
    {
        return MenuUtilities.AbsRect(pos1.x, pos1.y, pos2.x, pos2.y);
    }
     
    public static Rect AbsRect(float x1, float y1, float x2, float y2)
    {
        float num = y2 - y1;
        float num2 = x2 - x1;
        return new Rect(x1, y1, num, num2);
    }
     
    public static Texture2D TexClear = new Texture2D(1, 1);
}

