using SDG.Unturned;
using System.Reflection;



 
public class OV_PlayerUI
{
 
    [Override(typeof(PlayerUI), "updateCrosshair", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_updateCrosshair(float spread)
    {
        bool flag = !Provider.modeConfigData.Gameplay.Crosshair;
        if (!flag)
        {
            PlayerLifeUI.crosshairLeftImage.positionOffset_X = (int)(-spread * 400f) - 4;
            PlayerLifeUI.crosshairLeftImage.positionOffset_Y = -4;
            PlayerLifeUI.crosshairRightImage.positionOffset_X = (int)(spread * 400f) - 4;
            PlayerLifeUI.crosshairRightImage.positionOffset_Y = -4;
            PlayerLifeUI.crosshairUpImage.positionOffset_X = -4;
            PlayerLifeUI.crosshairUpImage.positionOffset_Y = (int)(-spread * 400f) - 4;
            PlayerLifeUI.crosshairDownImage.positionOffset_X = -4;
            PlayerLifeUI.crosshairDownImage.positionOffset_Y = (int)(spread * 400f) - 4;
        }
    }
}

