using SDG.Unturned;
using System.Reflection;
using UnityEngine;



public static class OV_DamageTool
{ 
    [Override(typeof(DamageTool), "raycast", BindingFlags.Static | BindingFlags.Public, 1)]
    public static RaycastInfo OV_raycast(Ray ray, float range, int mask)
    {
        switch (OV_DamageTool.OVType)
        {
            case OverrideType.Extended:
                return RaycastUtilities.GenerateOriginalRaycast(ray, MiscOptions.MeleeRangeExtension, mask);
            case OverrideType.PlayerHit:
                for (int i = 0; i < Provider.clients.Count; i++)
                {
                    bool flag = VectorUtilities.GetDistance(Player.player.transform.position, Provider.clients[i].player.transform.position) > 15.5;
                    if (!flag)
                    {
                        RaycastUtilities.GenerateRaycast(out RaycastInfo result);
                        return result;
                    }
                }
                break;
            case OverrideType.SilentAim:
                {
                    return RaycastUtilities.GenerateRaycast(out RaycastInfo raycastInfo) ? raycastInfo : RaycastUtilities.GenerateOriginalRaycast(ray, range, mask);
                }
            case OverrideType.SilentAimMelee:
                {
                    return RaycastUtilities.GenerateRaycast(out RaycastInfo raycastInfo2) ? raycastInfo2 : RaycastUtilities.GenerateOriginalRaycast(ray, MiscOptions.MeleeRangeExtension, mask);
                }
        }
        return RaycastUtilities.GenerateOriginalRaycast(ray, range, mask);
    }
     
    public static OverrideType OVType = OverrideType.None;
}

