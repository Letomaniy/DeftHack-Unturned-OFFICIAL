using SDG.Unturned;
using Steamworks;

using System;
using System.IO;
using System.Reflection;
using UnityEngine;



public class OV_Player : MonoBehaviour
{

    [Override(typeof(Player), "ReceiveTakeScreenshot", BindingFlags.Instance | BindingFlags.Public, 0)]
    
    public void OV_askScreenshot( )
    {
        
                base.StartCoroutine(PlayerCoroutines.TakeScreenshot());
             
    }

}