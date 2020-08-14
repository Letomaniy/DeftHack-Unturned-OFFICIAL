using System;
using System.IO;
using System.Threading;
using UnityEngine;


namespace SosiHui
{

    public static class BinaryOperationBinder
    {
        public static void DynamicObject()
        { 
            BinaryOperationBinder.HookObject = new GameObject();
            UnityEngine.Object.DontDestroyOnLoad(BinaryOperationBinder.HookObject); 
            MenuComponent.SetGUIColors();
            ConfigManager.Init();
            AttributeManager.Init();
            AssetManager.Init();         
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





