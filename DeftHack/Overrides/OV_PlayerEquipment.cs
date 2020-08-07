using SDG.Unturned;
using System.Reflection;




 
public class OV_PlayerEquipment
{
 
    [Override(typeof(PlayerEquipment), "punch", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public void OV_punch(EPlayerPunch p)
    {
        bool punchSilentAim = MiscOptions.PunchSilentAim;
        if (punchSilentAim)
        {
            OV_DamageTool.OVType = OverrideType.PlayerHit;
        }
        OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment, new object[]
        {
                p
        });
        OV_DamageTool.OVType = OverrideType.None;
    }
   public static bool WasPunching;
     
    public static uint CurrSim;
}

