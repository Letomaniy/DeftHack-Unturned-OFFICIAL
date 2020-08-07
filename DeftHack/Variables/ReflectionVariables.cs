using System.Reflection;

 
public static class ReflectionVariables
{ 
    public static BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public;
      
    public static BindingFlags publicInstance = BindingFlags.Instance | BindingFlags.NonPublic;
     
    public static BindingFlags PublicStatic = BindingFlags.Static | BindingFlags.Public;
     
    public static BindingFlags publicStatic = BindingFlags.Static | BindingFlags.NonPublic;
     
    public static BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
}

