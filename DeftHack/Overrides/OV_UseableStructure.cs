using SDG.Unturned;
using System.Reflection;
using UnityEngine;




public class OV_UseableStructure
{
  
    [Override(typeof(UseableStructure), "checkSpace", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public bool OV_checkSpace()
    {
        if (!MiscOptions.BuildinObstacles || PlayerCoroutines.IsSpying)
        {
            return (bool)OverrideUtilities.CallOriginal(this, new object[0]);
        }
        OverrideUtilities.CallOriginal(this, new object[0]);
        if ((Vector3)OV_UseableStructure.pointField.GetValue(this) != Vector3.zero && !MiscOptions.Freecam)
        {
            if (MiscOptions.epos)
            {
                OV_UseableStructure.pointField.SetValue(this, (Vector3)OV_UseableStructure.pointField.GetValue(this) + MiscOptions.pos);
            }
            return true;
        }
        if (Physics.Raycast(new Ray(OptimizationVariables.MainCam.transform.position, OptimizationVariables.MainCam.transform.forward), out RaycastHit raycastHit, 20f, RayMasks.DAMAGE_CLIENT))
        {
            Vector3 vector = raycastHit.point;
            if (MiscOptions.epos)
            {
                vector += MiscOptions.pos;
            }
            OV_UseableStructure.pointField.SetValue(this, vector);
            return true;
        }
        Vector3 vector2 = OptimizationVariables.MainCam.transform.position + OptimizationVariables.MainCam.transform.forward * 7f;
        if (MiscOptions.epos)
        {
            vector2 += MiscOptions.pos;
        }
        OV_UseableStructure.pointField.SetValue(this, vector2);
        return true;
    }

  
    public static FieldInfo pointField = typeof(UseableStructure).GetField("point", ReflectionVariables.publicInstance);
}
