using SDG.Unturned;
using System.Reflection;


 
public static class OV_PlayerPauseUI
{ 
    [Override(typeof(PlayerPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_onClickedExitButton(ISleekElement button)
    {
        Provider.disconnect();
    }
}

