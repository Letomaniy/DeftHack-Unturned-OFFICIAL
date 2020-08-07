

using UnityEngine;


 
public static class OV_Physics
{

    public static bool OV_Linecast(Vector3 start, Vector3 end, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
    {
        return !OV_Physics.ForceReturnFalse && (bool)OverrideUtilities.CallOriginal(null, new object[]
        {
                start,
                end,
                layerMask,
                queryTriggerInteraction
        });
    }
     
    public static bool ForceReturnFalse = false;
}

