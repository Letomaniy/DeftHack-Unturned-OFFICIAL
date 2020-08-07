using System.Collections.Generic;


 
public static class HotkeyOptions
{
    [Save]
    public static Dictionary<string, Dictionary<string, Hotkey>> HotkeyDict = new Dictionary<string, Dictionary<string, Hotkey>>();
     
    [Save]
    public static Dictionary<string, Hotkey> UnorganizedHotkeys = new Dictionary<string, Hotkey>();
}

