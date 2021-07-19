using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Component]
public class HotkeyComponent : MonoBehaviour
{
    public void Update()
    {

        if (HotkeyComponent.NeedsKeys)
        {
            List<KeyCode> currentKeys = HotkeyComponent.CurrentKeys.ToList<KeyCode>();
            HotkeyComponent.CurrentKeys.Clear();
            foreach (KeyCode keyCode in HotkeyComponent.Keys)
            {
                if (Input.GetKey(keyCode))
                {
                    HotkeyComponent.CurrentKeys.Add(keyCode);
                }
            }
            if (HotkeyComponent.CurrentKeys.Count < HotkeyComponent.CurrentKeyCount && HotkeyComponent.CurrentKeyCount > 0)
            {
                HotkeyComponent.CurrentKeys = currentKeys;
                HotkeyComponent.StopKeys = true;
            }
            HotkeyComponent.CurrentKeyCount = HotkeyComponent.CurrentKeys.Count;
        }
        if (!MenuComponent.IsInMenu)
        {
            foreach (KeyValuePair<string, Newtonsoft.Json.Serialization.Action> keyValuePair in HotkeyComponent.ActionDict)
            {
                if ((!MiscOptions.PanicMode || keyValuePair.Key == "_PanicButton") && HotkeyUtilities.IsHotkeyDown(keyValuePair.Key))
                {
                    keyValuePair.Value.Invoke();
                }
            }
            return;
        }

    }


    public static void Clear()
    {
        HotkeyComponent.NeedsKeys = false;
        HotkeyComponent.StopKeys = false;
        HotkeyComponent.CurrentKeyCount = 0;
        HotkeyComponent.CurrentKeys = new List<KeyCode>();
    }

    public static bool NeedsKeys;
    public static bool StopKeys;
    public static int CurrentKeyCount;
    public static List<KeyCode> CurrentKeys;
    public static Dictionary<string, Newtonsoft.Json.Serialization.Action> ActionDict = new Dictionary<string, Newtonsoft.Json.Serialization.Action>();
    public static KeyCode[] Keys = (KeyCode[])Enum.GetValues(typeof(KeyCode));
}

