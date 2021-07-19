using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;


 
public class OverrideWrapper
{
    
    public MethodInfo Original { get; set; }

   
    public MethodInfo Modified { get; set; }

    
    public IntPtr PtrOriginal { get; private set; }

    
    public IntPtr PtrModified { get; private set; }

     
    public OverrideUtilities.OffsetBackup OffsetBackup { get; private set; }

    
    public OverrideAttribute Attribute { get; set; }

    
    public bool Detoured { get; private set; }

     
    public object Instance { get; }

     
    public bool Local { get; private set; }
     
    public OverrideWrapper(MethodInfo original, MethodInfo modified, OverrideAttribute attribute, object instance = null)
    {
        try
        {
            Original = original;
            Modified = modified;
            Instance = instance;
            Attribute = attribute;
            Local = (Modified.DeclaringType.Assembly == Assembly.GetExecutingAssembly());
            RuntimeHelpers.PrepareMethod(original.MethodHandle);
            RuntimeHelpers.PrepareMethod(modified.MethodHandle);
            PtrOriginal = Original.MethodHandle.GetFunctionPointer();
            PtrModified = Modified.MethodHandle.GetFunctionPointer();
            OffsetBackup = new OverrideUtilities.OffsetBackup(PtrOriginal);
            Detoured = false;
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = new StreamWriter("1ov.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{ex.Message}         { ex.Source}        {ex.StackTrace}        {ex.Data.ToString()}/n{original.Name}");
            }
        }
    }


    public bool Override()
    {
        bool detoured = Detoured;
        bool result;
        if (detoured)
        {
            result = true;
        }
        else
        {
            bool flag = OverrideUtilities.OverrideFunction(PtrOriginal, PtrModified);
            bool flag2 = flag;
            if (flag2)
            {
                Detoured = true;
            }
            result = flag;
        }
        return result;
    }

    
    public bool Revert()
    {
        bool flag = !Detoured;
        bool result;
        if (flag)
        {
            result = false;
        }
        else
        {
            bool flag2 = OverrideUtilities.RevertOverride(OffsetBackup);
            bool flag3 = flag2;
            if (flag3)
            {
                Detoured = false;
            }
            result = flag2;
        }
        return result;
    }

     
    public object CallOriginal(object[] args, object instance = null)
    {
        Revert();
        object result = null;
        
            result = Original.Invoke(instance ?? Instance, args);

        
        Override();
        return result;
    }
}

