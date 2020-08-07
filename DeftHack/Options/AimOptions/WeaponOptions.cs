 
public static class WeaponOptions
{ 
    [Save]
    public static bool ShowWeaponInfo = false;
     
    [Save]
    public static bool CustomCrosshair = false;
     
    [Save]
    public static SerializableColor CrosshairColor = new SerializableColor(255, 0, 0);
      
    [Save]
    public static bool NoRecoil = false;
     
    [Save]
    public static bool NoSpread = false;
     
    [Save]
    public static bool NoSway = false;
     
    [Save]
    public static bool NoDrop = false;
     
    [Save]
    public static bool OofOnDeath = false;
     
    [Save]
    public static bool AutoReload = false;
     
    [Save]
    public static bool Tracers = false;
     
    [Save]
    public static bool EnableBulletDropPrediction = false;
     
    [Save]
    public static bool HighlightBulletDropPredictionTarget = false;

    [Save]
    public static bool Zoom;

    [Save]
    public static float ZoomValue = 16f;

}

