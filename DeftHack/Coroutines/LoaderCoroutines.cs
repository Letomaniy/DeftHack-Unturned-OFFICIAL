using System;
using System.Collections;
using System.IO;
using System.Threading;
using UnityEngine;

public static class LoaderCoroutines
{
     
    public static IEnumerator LoadAssets()
    {
        yield return new WaitForSeconds(1f);
        byte[] Loader = File.ReadAllBytes(LoaderCoroutines.AssetPath);
        Console.WriteLine(LoaderCoroutines.AssetPath);
        if (File.Exists(AssetPath))
        {
            AssetBundle bundle = AssetBundle.LoadFromMemory(Loader);
            AssetVariables.ABundle = bundle;
            foreach (Shader s in bundle.LoadAllAssets<Shader>())
            {
                AssetVariables.Materials.Add(s.name, new Material(s)
                {
                    hideFlags = HideFlags.HideAndDontSave
                });

            }
            foreach (Shader s2 in bundle.LoadAllAssets<Shader>())
            {
                AssetVariables.Shaders.Add(s2.name, s2);

            }
            foreach (Font f in bundle.LoadAllAssets<Font>())
            {
                AssetVariables.Fonts.Add(f.name, f);

            }
            foreach (AudioClip ac in bundle.LoadAllAssets<AudioClip>())
            {
                AssetVariables.Audio.Add(ac.name, ac);

            }
            foreach (Texture2D t in bundle.LoadAllAssets<Texture2D>())
            {
                bool flag = t.name != "Font Texture";
                if (flag)
                {
                    AssetVariables.Textures.Add(t.name, t);
                }
            }
            ESPComponent.GLMat = AssetVariables.Materials["ESP"];
            ESPComponent.ESPFont = AssetVariables.Fonts["Roboto-Light"];
            MenuComponent._TabFont = AssetVariables.Fonts["Anton-Regular"];
            MenuComponent._TextFont = AssetVariables.Fonts["CALIBRI"];
            ESPCoroutines.Normal = Shader.Find("Standard");
            ESPCoroutines.LitChams = AssetVariables.Shaders["chamsLit"];
            ESPCoroutines.UnlitChams = AssetVariables.Shaders["chamsUnlit"];
            LoaderCoroutines.IsLoaded = true;
             ConfigManager.Init();
             MenuComponent.SetGUIColors();            
        }
        else
        {
            yield return (null);
        }
        yield break;
    }

    public static void Trash()
    {
        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            while (true)
            {
                GC.Collect();
                Thread.Sleep(300000);
            }

        }).Start();
    } 
    public static bool IsLoaded;
     
    public static string AssetPath = Application.dataPath + "/assets";
}  
