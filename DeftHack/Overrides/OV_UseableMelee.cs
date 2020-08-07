using SDG.Unturned;
using System.Reflection;




 
public class OV_UseableMelee
{
    
    [Override(typeof(UseableMelee), "fire", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public static void OV_fire()
    {
        OV_DamageTool.OVType = OverrideType.None;
        bool flag = RaycastOptions.Enabled && MiscOptions.ExtendMeleeRange;
        if (flag)
        {
            OV_DamageTool.OVType = OverrideType.SilentAimMelee;
        }
        else
        {
            bool enabled = RaycastOptions.Enabled;
            if (enabled)
            {
                OV_DamageTool.OVType = OverrideType.SilentAim;
            }
            else
            {
                bool extendMeleeRange = MiscOptions.ExtendMeleeRange;
                if (extendMeleeRange)
                {
                    OV_DamageTool.OVType = OverrideType.Extended;
                }
            }
        }
        OverrideUtilities.CallOriginal(OptimizationVariables.MainPlayer.equipment.useable, new object[0]);
        OV_DamageTool.OVType = OverrideType.None;
    }
}

