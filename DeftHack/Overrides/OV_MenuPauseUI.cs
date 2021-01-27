using SDG.Unturned;
using System.Reflection;
using UnityEngine;


 
public static class OV_MenuPauseUI
{ 
    [Override(typeof(MenuPauseUI), "onClickedExitButton", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_onClickedExitButton(ISleekButton button)
    {
        Application.Quit();
    }
}

