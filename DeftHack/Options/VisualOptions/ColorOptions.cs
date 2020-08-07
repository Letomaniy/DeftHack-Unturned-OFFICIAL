using System.Collections.Generic;


using UnityEngine;


 
public static class ColorOptions
{
   
    [Save]
    public static Dictionary<string, ColorVariable> ColorDict = new Dictionary<string, ColorVariable>();

     
    public static Dictionary<string, ColorVariable> DefaultColorDict = new Dictionary<string, ColorVariable>();

 
    public static ColorVariable errorColor = new ColorVariable("errorColor", "#ERROR.NOTFOUND", Color.magenta, true);

    
    public static ColorVariable preview = new ColorVariable("preview", "No Color Selected", Color.white, true);

 
    public static ColorVariable previewselected;

     
    public static string selectedOption;
}