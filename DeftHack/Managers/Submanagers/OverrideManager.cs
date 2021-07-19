using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
 
public class OverrideManager
{ 
    public Dictionary<OverrideAttribute, OverrideWrapper> Overrides => OverrideManager._overrides;
    public void OffHook()
    {
        foreach (OverrideWrapper overrideWrapper in Overrides.Values)
        {
            overrideWrapper.Revert();
        }
    }
 
    public void LoadOverride(MethodInfo method)
    {
        try
        {
            OverrideAttribute attribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
            bool flag = Overrides.Count((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Key.Method == attribute.Method) > 0;
            if (!flag)
            {
                OverrideWrapper overrideWrapper = new OverrideWrapper(attribute.Method, method, attribute, null);
                overrideWrapper.Override();
                Overrides.Add(attribute, overrideWrapper);
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = new StreamWriter("1ov.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{ex.Message}         { ex.Source}        {ex.StackTrace}        {ex.Data.ToString()}/n{method.Name}");
            }
        }
    }


    public void InitHook()
    {
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            foreach (MethodInfo methodInfo in type.GetMethods())
            {
                bool flag = methodInfo.Name == "OV_GetKey" && methodInfo.IsDefined(typeof(OverrideAttribute), false);
                if (flag)
                {
                    LoadOverride(methodInfo);
                }
            }
        }
    }

 
    public static Dictionary<OverrideAttribute, OverrideWrapper> _overrides = new Dictionary<OverrideAttribute, OverrideWrapper>();
}

