using SDG.Unturned;
using System.Reflection;



 
public static class OV_PlayerLifeUI
{
 
    [Override(typeof(PlayerLifeUI), "hasCompassInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static bool OV_hasCompassInInventory()
    {
        bool compass = MiscOptions.Compass;
        return compass || (bool)OverrideUtilities.CallOriginal(null, new object[0]);
    }

    
    [Override(typeof(PlayerLifeUI), "updateGrayscale", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_updateGrayscale()
    {
        bool noGrayscale = MiscOptions.NoGrayscale;
        if (!noGrayscale)
        {
            OverrideUtilities.CallOriginal(null, new object[0]);
        }
    }
     
    [OnSpy]
    public static void Disable()
    {
        bool flag = !DrawUtilities.ShouldRun();
        if (!flag)
        {
            OV_PlayerLifeUI.WasCompassEnabled = MiscOptions.Compass;
            MiscOptions.Compass = false;
            PlayerLifeUI.updateCompass();
        }
    }
     
    [OffSpy]
    public static void Enable()
    {
        bool flag = !DrawUtilities.ShouldRun();
        if (!flag)
        {
            MiscOptions.Compass = OV_PlayerLifeUI.WasCompassEnabled;
            PlayerLifeUI.updateCompass();
        }
    }
     
    public static bool WasCompassEnabled;
}

