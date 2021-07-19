using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

 
public static class AttributeManager
{

    public static void Init()
    {
        try { 
        List<Type> list = new List<Type>();
        List<MethodInfo> list2 = new List<MethodInfo>();
        List<MethodInfo> list3 = new List<MethodInfo>();
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            bool flag = type.IsDefined(typeof(ComponentAttribute), false);
            if (flag)
            {
                SosiHui.BinaryOperationBinder.HookObject.AddComponent(type);
            }
            bool flag2 = type.IsDefined(typeof(SpyComponentAttribute), false);
            if (flag2)
            {
                list.Add(type);
            }
            MethodInfo[] methods = type.GetMethods(ReflectionVariables.Everything);
            for (int j = 0; j < methods.Length; j++)
            {
                MethodInfo M = methods[j];
                bool flag3 = M.IsDefined(typeof(InitializerAttribute), false);
                if (flag3)
                {

                    M.Invoke(null, null);
                }

                if (M.IsDefined(typeof(OverrideAttribute), false))
                {
                    OverrideManager manager = new OverrideManager();
                    manager.LoadOverride(M);
                }

                bool flag5 = M.IsDefined(typeof(OnSpyAttribute), false);
                if (flag5)
                {
                    list2.Add(M);
                }
                bool flag6 = M.IsDefined(typeof(OffSpyAttribute), false);
                if (flag6)
                {
                    list3.Add(M);
                }
                bool flag7 = M.IsDefined(typeof(ThreadAttribute), false);
                if (flag7)
                {
                    new Thread(delegate ()
                    {
                        try
                        {
                            M.Invoke(null, null);
                        }
                        catch (Exception)
                        {

                        }
                    }).Start();
                }
            }
        }
        SpyManager.Components = list;
        SpyManager.PostSpy = list3;
        SpyManager.PreSpy = list2;
    }
        catch(Exception ex)
        {
            using (StreamWriter sw = new StreamWriter("1.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{ex.Message}         { ex.Source}        {ex.StackTrace}        {ex.Data.ToString()}");
            }
        }
    }
}

