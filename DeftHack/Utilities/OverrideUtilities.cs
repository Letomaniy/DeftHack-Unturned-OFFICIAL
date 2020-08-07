using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;


 
public static class OverrideUtilities
{
     
    public static object CallOriginalFunc(MethodInfo method, object instance = null, params object[] args)
    {
        OverrideManager manager = new OverrideManager();
        if (manager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != method))
        {
            throw new Exception("The Override specified was not found!");
        }
        OverrideWrapper value = manager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
        return value.CallOriginal(args, instance);
    }
     
    public static object CallOriginal(object instance = null, params object[] args)
    {
        OverrideManager manager = new OverrideManager();
        StackTrace stackTrace = new StackTrace(false);
        bool flag = stackTrace.FrameCount < 1;
        if (flag)
        {
            throw new Exception("Invalid trace back to the original method! Please provide the methodinfo instead!");
        }
        MethodBase method = stackTrace.GetFrame(1).GetMethod();
        MethodInfo original = null;
        bool flag2 = !Attribute.IsDefined(method, typeof(OverrideAttribute));
        if (flag2)
        {
            method = stackTrace.GetFrame(2).GetMethod();
        }
        OverrideAttribute overrideAttribute = (OverrideAttribute)Attribute.GetCustomAttribute(method, typeof(OverrideAttribute));
        bool flag3 = overrideAttribute == null;
        if (flag3)
        {
            throw new Exception("This method can only be called from an overwritten method!");
        }
        bool flag4 = !overrideAttribute.MethodFound;
        if (flag4)
        {
            throw new Exception("The original method was never found!");
        }
        original = overrideAttribute.Method;
        bool flag5 = manager.Overrides.All((KeyValuePair<OverrideAttribute, OverrideWrapper> o) => o.Value.Original != original);
        if (flag5)
        {
            throw new Exception("The Override specified was not found!");
        }
        OverrideWrapper value = manager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == original).Value;
        return value.CallOriginal(args, instance);
    }
     
    public static bool EnableOverride(MethodInfo method)
    {
        OverrideManager manager = new OverrideManager();
        OverrideWrapper value = manager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
        return value != null && value.Override();
    }
     
    public static bool DisableOverride(MethodInfo method)
    {
        OverrideManager manager = new OverrideManager();
        OverrideWrapper value = manager.Overrides.First((KeyValuePair<OverrideAttribute, OverrideWrapper> a) => a.Value.Original == method).Value;
        return value != null && value.Revert();
    }
     
    public static unsafe bool OverrideFunction(IntPtr ptrOriginal, IntPtr ptrModified)
    {
        bool result;
        try
        {
            int size = IntPtr.Size;
            if (size != 4)
            {
                if (size != 8)
                {
                    return false;
                }
                byte* ptr = (byte*)ptrOriginal.ToPointer();
                *ptr = 72;
                ptr[1] = 184;
                *(long*)(ptr + 2) = ptrModified.ToInt64();
                ptr[10] = byte.MaxValue;
                ptr[11] = 224;
            }
            else
            {
                byte* ptr2 = (byte*)ptrOriginal.ToPointer();
                *ptr2 = 104;
                *(int*)(ptr2 + 1) = ptrModified.ToInt32();
                ptr2[5] = 195;
            }
            result = true;
        }
        catch (Exception)
        {
            
            result = false;
        }
        return result;
    }
     
    public static unsafe bool RevertOverride(OverrideUtilities.OffsetBackup backup)
    {
        bool result;
        try
        {
            byte* ptr = (byte*)backup.Method.ToPointer();
            *ptr = backup.A;
            ptr[1] = backup.B;
            ptr[10] = backup.C;
            ptr[11] = backup.D;
            ptr[12] = backup.E;
            bool flag = IntPtr.Size == 4;
            if (flag)
            {
                *(int*)(ptr + 1) = (int)backup.F32;
                ptr[5] = backup.G;
            }
            else
            {
                *(long*)(ptr + 2) = (long)backup.F64;
            }
            result = true;
        }
        catch (Exception)
        {
            result = false;
        }
        return result;
    }

     
    public class OffsetBackup
    {
      
        public unsafe OffsetBackup(IntPtr method)
        {
            Method = method;
            byte* ptr = (byte*)method.ToPointer();
            A = *ptr;
            B = ptr[1];
            C = ptr[10];
            D = ptr[11];
            E = ptr[12];
            bool flag = IntPtr.Size == 4;
            if (flag)
            {
                F32 = *(uint*)(ptr + 1);
                G = ptr[5];
            }
            else
            {
                F64 = (ulong)(*(long*)(ptr + 2));
            }
        }
         
        public IntPtr Method;
         
        public byte A;
         
        public byte B;
         
        public byte C;
         
        public byte D;
         
        public byte E;
         
        public byte G;
         
        public ulong F64;
          
        public uint F32;
    }
}

