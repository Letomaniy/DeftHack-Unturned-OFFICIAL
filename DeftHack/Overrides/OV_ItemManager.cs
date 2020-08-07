using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

 
public static class OV_ItemManager
{
   
    [Override(typeof(ItemManager), "getItemsInRadius", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_getItemsInRadius(Vector3 center, float sqrRadius, List<RegionCoordinate> search, List<InteractableItem> result)
    {
        if (MiscOptions.IncreaseNearbyItemDistance)
        {
            OverrideUtilities.CallOriginal(null, new object[]
            {
                    center,
                    Mathf.Pow(20f, 2f),
                    search,
                    result
            });
        }
        else
        {
            OverrideUtilities.CallOriginal(null, new object[]
            {
                    center,
                    sqrRadius,
                    search,
                    result
            });
        }
    }
}

