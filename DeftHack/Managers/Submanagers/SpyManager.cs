using System;
using System.Collections.Generic;
using System.Reflection;
 
public class SpyManager
{ 
    public static void InvokePre()
    {
        foreach (MethodInfo methodInfo in SpyManager.PreSpy)
        {
            methodInfo.Invoke(null, null);
        }
    }
     
    public static void InvokePost()
    {
        foreach (MethodInfo methodInfo in SpyManager.PostSpy)
        {
            methodInfo.Invoke(null, null);
        }
    }
     
    public static void DestroyComponents()
    {
        foreach (Type type in SpyManager.Components)
        {
            UnityEngine.Object.Destroy(SosiHui.BinaryOperationBinder.HookObject.GetComponent(type));
        }
    }
     
    public static void AddComponents()
    {
        foreach (Type type in SpyManager.Components)
        {
            SosiHui.BinaryOperationBinder.HookObject.AddComponent(type);
        }
    }
     
    public static IEnumerable<MethodInfo> PreSpy;
     
    public static IEnumerable<Type> Components;
     
    public static IEnumerable<MethodInfo> PostSpy;
}

