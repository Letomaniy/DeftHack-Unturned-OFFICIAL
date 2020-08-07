using System.Collections.Generic;

using System.Net;
using UnityEngine;
 
public static class MathUtilities
{
    private static readonly WebClient web = new WebClient();
    
    [Initializer]
    public static void GenerateRandom()
    {
        MathUtilities.Random = new System.Random();
    }
     
    public static bool Intersect(Vector3 p0, Vector3 p1, Vector3 oVector, Vector3 bCenter, out Vector3 intersection)
    {
        intersection = Vector3.zero;
        Vector3 vector = p1 - p0;
        float num = Vector3.Dot(vector, oVector);
        bool flag = num == 0f;
        bool result;
        if (flag)
        {
            result = false;
        }
        else
        {
            float num2 = Vector3.Dot(p0 - bCenter, oVector);
            float num3 = -(num2 / num);
            intersection = p0 + num3 * vector;
            result = true;
        }
        return result;
    }
     
    public static Vector3 GetOrthogonalVector(Vector3 vCenter, Vector3 vPoint)
    {
        Vector3 vector = vCenter - vPoint;
        double distance = VectorUtilities.GetDistance(vCenter, vPoint);
        return vector / (float)distance;
    }
     
    public static Vector3[] GetRectanglePoints(Vector3 playerPos, Vector3[] bCorners, Bounds bound)
    {
        Vector3 orthogonalVector = MathUtilities.GetOrthogonalVector(bound.center, playerPos);
        List<Vector3> list = new List<Vector3>();
        Vector3[] array = new Vector3[]
        {
                bCorners[0],
                bCorners[1],
                bCorners[1],
                bCorners[3],
                bCorners[3],
                bCorners[2],
                bCorners[2],
                bCorners[0],
                bCorners[4],
                bCorners[5],
                bCorners[5],
                bCorners[7],
                bCorners[7],
                bCorners[6],
                bCorners[6],
                bCorners[4],
                bCorners[0],
                bCorners[4],
                bCorners[1],
                bCorners[5],
                bCorners[2],
                bCorners[6],
                bCorners[3],
                bCorners[7]
        };
        for (int i = 0; i < 24; i += 2)
        {
            Vector3 p = array[i];
            Vector3 p2 = array[i + 1];
            bool flag = MathUtilities.Intersect(p, p2, orthogonalVector, bound.center, out Vector3 item);
            if (flag)
            {
                list.Add(item);
            }
        }
        Bounds bounds;
        bounds = new Bounds(bound.center, bound.size * 1.2f);
        for (int j = list.Count - 1; j > -1; j--)
        {
            bool flag2 = !bounds.Contains(list[j]);
            if (flag2)
            {
                list.RemoveAt(j);
            }
        }
        return list.ToArray();
    }

     
    public static System.Random Random;
}

