using System;
using System.Collections.Generic;
using System.Linq;





using UnityEngine;


 
public static class ESPOptions
{ 
    [Save]
    public static bool Enabled = true;
     
    [Save]
    public static bool ChamsEnabled = false;
     
    [Save]
    public static bool ChamsFlat = false;
     
    [Save]
    public static bool ShowVanishPlayers = false;

    [Save]
    public static bool ShowToolTipWindow = false;

    [Save]
    public static bool ShowCoordinates = false;
     
    [Save]
    public static ESPVisual[] VisualOptions = Enumerable.Repeat<ESPVisual>(new ESPVisual
    {
        Enabled = false,
        Labels = false,
        Boxes = false,
        ShowName = false,
        ShowDistance = false,
        ShowAngle = false,
        TwoDimensional = false,
        Glow = false,
        InfiniteDistance = false,
        LineToObject = false,
        TextScaling = false,
        UseObjectCap = false,
        CustomTextColor = false,
        Distance = 250f,
        Location = LabelLocation.BottomMiddle,
        FixedTextSize = 11,
        MinTextSize = 8,
        MaxTextSize = 11,
        MinTextSizeDistance = 800f,
        BorderStrength = 2,
        ObjectCap = 24
    }, Enum.GetValues(typeof(ESPTarget)).Length).ToArray<ESPVisual>();
     
    [Save]
    public static Dictionary<ESPTarget, int> PriorityTable = Enum.GetValues(typeof(ESPTarget)).Cast<ESPTarget>().ToDictionary((ESPTarget x) => x, (ESPTarget x) => (int)x);
     
    [Save]
    public static bool ShowPlayerWeapon = false;
     
    [Save]
    public static bool ShowPlayerVehicle = false;
     
    [Save]
    public static bool UsePlayerGroup = false;
     
    [Save]
    public static SerializableColor SameGroupColor = Extensions.ToSerializableColor(Color.green);
      
    [Save]
    public static bool FilterItems = false;
     
    [Save]
    public static bool ShowVehicleFuel;
     
    [Save]
    public static bool ShowVehicleHealth;
     
    [Save]
    public static bool ShowVehicleLocked;
     
    [Save]
    public static bool FilterVehicleLocked;
     
    [Save]
    public static bool ShowSentryItem;
     
    [Save]
    public static bool ShowClaimed;
     
    [Save]
    public static bool ShowGeneratorFuel;
     
    [Save]
    public static bool ShowGeneratorPowered;
}

