using SDG.Unturned;
using Steamworks;
using System.Reflection;



 
public static class OV_Provider
{
  
    [Override(typeof(Provider), "receiveClient", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void OV_receiveClient(CSteamID steamID, byte[] packet, int offset, int size, int channel)
    {
        bool flag = !OV_Provider.IsConnected;
        if (flag)
        {
            OV_Provider.IsConnected = true;
        }
        
    }
     
    public static bool IsConnected;
}

