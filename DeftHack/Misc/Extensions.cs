using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

 
public static class Extensions
{
 
    public static Color Invert(this Color32 color)
    {
        return new Color(byte.MaxValue - color.r, byte.MaxValue - color.g, byte.MaxValue - color.b);
    }

 
    public static SerializableColor ToSerializableColor(this Color32 c)
    {
        return new SerializableColor(c.r, c.g, c.b, c.a);
    }

    
    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
    {
        return source.Skip(Math.Max(0, source.Count<T>() - N));
    }

   
    public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> target)
    {
        foreach (T item in target)
        {
            source.Add(item);
        }
    }
}
