using SDG.Unturned;



 
public static class AimbotOptions
{ 
    [Save]
    public static bool Enabled = false;
     
    [Save]
    public static bool UseGunDistance = false;
     
    [Save]
    public static bool Smooth = false;
     
    [Save]
    public static bool OnKey = false;

    [Save]
    public static bool UseFovAim = true;
     
    public static float MaxSpeed = 20f;
      
    [Save]
    public static float AimSpeed = 5f;
     
    [Save]
    public static float Distance = 300f;
     
    [Save]
    public static float FOV = 15f;
     
    [Save]
    public static ELimb TargetLimb = ELimb.SKULL;
     
    [Save]
    public static TargetMode TargetMode = TargetMode.Distance;
     
    [Save]
    public static bool NoAimbotDrop = false;
}

