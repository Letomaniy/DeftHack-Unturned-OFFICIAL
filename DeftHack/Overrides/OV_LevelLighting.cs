using SDG.Unturned;
using System.Reflection;





 
public static class OV_LevelLighting
{
 
    [OnSpy]
    public static void Disable()
    {
        bool flag = !DrawUtilities.ShouldRun();
        if (!flag)
        {
            OV_LevelLighting.WasEnabled = MiscOptions.ShowPlayersOnMap;
            MiscOptions.ShowPlayersOnMap = false;
            OV_LevelLighting.OV_updateLighting();
        }
    }

 
    [OffSpy]
    public static void Enable()
    {
        bool flag = !DrawUtilities.ShouldRun();
        if (!flag)
        {
            MiscOptions.ShowPlayersOnMap = OV_LevelLighting.WasEnabled;
            OV_LevelLighting.OV_updateLighting();
        }
    }

 
    [Initializer]
    public static void Init()
    {
        OV_LevelLighting.Time = typeof(LevelLighting).GetField("_time", BindingFlags.Static | BindingFlags.NonPublic);
    }

    
    [Override(typeof(LevelLighting), "updateLighting", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_updateLighting()
    {
        float time = LevelLighting.time;
        bool flag = !DrawUtilities.ShouldRun() || !MiscOptions.SetTimeEnabled || PlayerCoroutines.IsSpying;
        if (flag)
        {
            OverrideUtilities.CallOriginal(null, new object[0]);
        }
        else
        {
            OV_LevelLighting.Time.SetValue(null, MiscOptions.Time);
            OverrideUtilities.CallOriginal(null, new object[0]);
            OV_LevelLighting.Time.SetValue(null, time);
        }
    }
     
    public static FieldInfo Time;
     
    public static bool WasEnabled;
}

