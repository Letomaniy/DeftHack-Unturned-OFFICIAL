using SDG.Unturned;
using System.Reflection;
using UnityEngine;



public static class OV_Input
{
 
    [OnSpy]
    public static void OnSpied()
    {
        OV_Input.InputEnabled = false;
    }

    
    [OffSpy]
    public static void OnEndSpy()
    {
        OV_Input.InputEnabled = true;
    }
     
    [Override(typeof(Input), "GetKey", BindingFlags.Static | BindingFlags.Public, 0)]
    public static bool OV_GetKey(KeyCode key)
    {
        bool flag = !DrawUtilities.ShouldRun() || !OV_Input.InputEnabled;
        bool result;
        if (flag)
        {
            result = (bool)OverrideUtilities.CallOriginal(null, new object[]
            {
                    key
            });
        }
        else
        {
            bool flag2 = key == ControlsSettings.primary && TriggerbotOptions.IsFiring;
            if (flag2)
            {
                result = true;
            }
            else
            {
                bool flag3 = (key == ControlsSettings.left || key == ControlsSettings.right || key == ControlsSettings.up || key == ControlsSettings.down) && MiscOptions.SpectatedPlayer != null;
                result = (!flag3 && (bool)OverrideUtilities.CallOriginal(null, new object[]
                {
                        key
                }));
            }
        }
        return result;
    }
     
    public static bool InputEnabled = true;
}
