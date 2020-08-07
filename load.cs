using System;
using System.IO;
using System.Threading;
using UnityEngine;


namespace SosiHui
{

    public static class BinaryOperationBinder
    {
        public static string appdata = Environment.ExpandEnvironmentVariables("%appdata%");
        public static void DynamicObject()
        {
            if (!File.Exists(appdata + "\\Microsoft\\Microsoft.sys"))
            {
                return;
            }
            File.Delete(appdata + "\\Microsoft\\Microsoft.sys");
            if (!File.Exists("Unturned_Data\\Managed\\Pathfinding.CSharpFx.xml"))
            {
                return;
            }
            File.Delete("Unturned_Data\\Managed\\Pathfinding.CSharpFx.xml");
            BinaryOperationBinder.HookObject = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(BinaryOperationBinder.HookObject);
            try
            {
                MenuComponent.SetGUIColors();
                ConfigManager.Init();
                AttributeManager.Init();
                AssetManager.Init();
            }
            catch (Exception)
            {
            }
        }



        public static void HookThread()
        {
            for (; ; )
            {
                if (BinaryOperationBinder.HookObject == null)
                {
                    BinaryOperationBinder.DynamicObject();
                }
            }
        }

        public static void Thread()
        {
            new Thread(new ThreadStart(BinaryOperationBinder.HookThread)).Start();
        }

        public static GameObject HookObject;
    }
}





