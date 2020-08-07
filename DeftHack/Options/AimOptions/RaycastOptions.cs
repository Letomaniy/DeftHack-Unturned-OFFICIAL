using SDG.Unturned;
using System.Collections.Generic;




 
public static class RaycastOptions
{
    [Save]
    public static bool Enabled = false;
    [Save]
    public static bool NoShootthroughthewalls = false;
    [Save]
    public static bool AlwaysHitHead = false;
     
    [Save]
    public static bool UseRandomLimb = false;
     
    [Save]
    public static bool UseCustomLimb = false;
     
    [Save]
    public static bool UseTargetMaterial = false;
     
    [Save]
    public static bool UseModifiedVector = false;
     
    [Save]
    public static bool EnablePlayerSelection = false;
     
    [Save]
    public static bool OnlyShootAtSelectedPlayer = false;
     
    [Save]
    public static float SelectedFOV = 10f;
     
    [Save]
    public static bool SilentAimUseFOV = false;

    [Save]
    public static bool ShowSilentAimUseFOV = false;

    [Save]
    public static bool ShowAimUseFOV = false;
 
    [Save]
    public static float SilentAimFOV = 10f;
     
    [Save]
    public static HashSet<TargetPriority> Targets = new HashSet<TargetPriority>
    {
    };

    [Save]
    public static TargetPriority Target = TargetPriority.Players;
     
    [Save]
    public static EPhysicsMaterial TargetMaterial = EPhysicsMaterial.ALIEN_DYNAMIC;
     
    [Save]
    public static ELimb TargetLimb = ELimb.SKULL;
       
    [Save]
    public static SerializableVector TargetRagdoll = new SerializableVector(0f, 10f, 0f);
}

