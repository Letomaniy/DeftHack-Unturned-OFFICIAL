using SDG.Unturned;
using Steamworks;
using System.Reflection;
using UnityEngine;


 
public class OV_Player : MonoBehaviour
{
 
    [Override(typeof(Player), "askScreenshot", BindingFlags.Instance | BindingFlags.Public, 0)]
    public void OV_askScreenshot(CSteamID steamid)
    {
        bool flag = OptimizationVariables.MainPlayer.channel.checkServer(steamid);
        if (flag)
        {
            base.StartCoroutine(PlayerCoroutines.TakeScreenshot());
        }
    }
}

