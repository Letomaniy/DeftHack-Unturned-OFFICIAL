using System.Collections.Generic;
using UnityEngine;

 
public static class AssetVariables
{
    
    public static AssetBundle ABundle;

    
    public static Dictionary<string, Material> Materials = new Dictionary<string, Material>();

     
    public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

 
    public static Dictionary<string, AudioClip> Audio = new Dictionary<string, AudioClip>();

  
    public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

   
    public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
}

