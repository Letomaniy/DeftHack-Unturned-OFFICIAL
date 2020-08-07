using UnityEngine;

 
public class SerializableColor
{
    
     
    public SerializableColor(int nr, int ng, int nb, int na)
    {
        r = nr;
        g = ng;
        b = nb;
        a = na;
    }
     
    public SerializableColor(int nr, int ng, int nb)
    {
        r = nr;
        g = ng;
        b = nb;
        a = 255;
    }
     
    public static implicit operator Color32(SerializableColor color)
    {
        return color.ToColor();
    }
     
    public static implicit operator Color(SerializableColor color)
    {
        return color.ToColor();
    }
     
    public static implicit operator SerializableColor(Color32 color)
    {
        return color.ToSerializableColor();
    }
     
    public Color32 ToColor()
    {
        return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
    }
     
    public int r;
     
    public int g;
     
    public int b;
     
    public int a;
}