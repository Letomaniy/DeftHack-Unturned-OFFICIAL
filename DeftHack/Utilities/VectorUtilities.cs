using System;

using UnityEngine;
 
public static class VectorUtilities
{
    
    public static double GetDistance(Vector3 point)
    {
        return VectorUtilities.GetDistance(OptimizationVariables.MainCam.transform.position, point);
    }
      
    public static double GetDistance(Vector3 start, Vector3 point)
    {
        Vector3 vector;
        vector.x = start.x - point.x;
        vector.y = start.y - point.y;
        vector.z = start.z - point.z;
        return Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }
     
    public static double GetMagnitude(Vector3 vector)
    {
        return Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }
     
    public static Vector3 Normalize(Vector3 vector)
    {
        return vector / (float)VectorUtilities.GetMagnitude(vector);
    }
     
    public static double GetAngleDelta(Vector3 mainPos, Vector3 forward, Vector3 target)
    {
        Vector3 vector = VectorUtilities.Normalize(target - mainPos);
        return Math.Atan2(VectorUtilities.GetMagnitude(Vector3.Cross(vector, forward)), Vector3.Dot(vector, forward)) * 57.295779513082323;
    }
}

