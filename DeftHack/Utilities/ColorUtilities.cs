using System.Globalization;
using System.Linq;



using UnityEngine;

 
public static class ColorUtilities
{
     
    public static void addColor(ColorVariable ColorVariable)
    {
        bool flag = !ColorOptions.DefaultColorDict.ContainsKey(ColorVariable.identity);
        if (flag)
        {
            ColorOptions.DefaultColorDict.Add(ColorVariable.identity, ColorVariable);
        }
    }
 
    public static ColorVariable getColor(string identifier)
    {
        bool flag = ColorOptions.ColorDict.TryGetValue(identifier, out ColorVariable colorVariable);
        ColorVariable result;
        if (flag)
        {
            result = colorVariable;
        }
        else
        {
            result = ColorOptions.errorColor;
        }
        return result;
    }

  
    public static string getHex(string identifier)
    {
        bool flag = ColorOptions.ColorDict.TryGetValue(identifier, out ColorVariable color);
        string result;
        if (flag)
        {
            result = ColorUtilities.ColorToHex(color);
        }
        else
        {
            result = ColorUtilities.ColorToHex(ColorOptions.errorColor);
        }
        return result;
    }

 
    public static void setColor(string identifier, Color32 color)
    {
        bool flag = ColorOptions.ColorDict.TryGetValue(identifier, out ColorVariable colorVariable);
        if (flag)
        {
            colorVariable.color = color.ToSerializableColor();
        }
    }

     
    public static string ColorToHex(Color32 color)
    {
        return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + "FF";
    }

 
    public static ColorVariable[] getArray()
    {
        return ColorOptions.ColorDict.Values.ToArray<ColorVariable>();
    }

 
    public static Color32 HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
        return new Color32(r, g, b, byte.MaxValue);
    }
}