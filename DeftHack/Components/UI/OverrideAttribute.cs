using System;
using System.Linq;
using System.Reflection;
[AttributeUsage(AttributeTargets.Method)]
public class OverrideAttribute : Attribute
{
    public Type Class { get; private set; }
    public string MethodName { get; private set; }
    public MethodInfo Method { get; private set; }
    public BindingFlags Flags { get; private set; }
    public bool MethodFound { get; private set; }
    public OverrideAttribute(Type tClass, string method, BindingFlags flags, int index = 0)
    {
        Class = tClass;
        MethodName = method;
        Flags = flags;
        try
        {
            Method = (from a in Class.GetMethods(flags)
                      where a.Name == method
                      select a).ToArray<MethodInfo>()[index];
            MethodFound = true;
        }
        catch (Exception)
        {
            MethodFound = false;
        }
    }
}

