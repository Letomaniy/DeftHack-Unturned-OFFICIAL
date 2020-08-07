using SDG.Unturned;
using System.Linq;
using UnityEngine;

 
public static class SphereUtilities
{ 
    public static bool GetRaycast(GameObject Target, Vector3 StartPos, out Vector3 Point)
    {
        Point = Vector3.zero;
        bool flag = Target == null;
        bool result;
        if (flag)
        {
            result = false;
        }
        else
        {
            int layer = Target.layer;
            Target.layer = LayerMasks.AGENT;
            RaycastComponent Component = Target.GetComponent<RaycastComponent>();
            bool flag2 = VectorUtilities.GetDistance(Target.transform.position, StartPos) <= 15.5;
            if (flag2)
            {
                Point = OptimizationVariables.MainPlayer.transform.position;
                result = true;
            }
            else
            {
                Vector3[] vertices = Component.Sphere.GetComponent<MeshCollider>().sharedMesh.vertices;
                foreach (Vector3 vector in (from v in vertices
                                            select Component.Sphere.transform.TransformPoint(v)).ToArray<Vector3>())
                {
                    Vector3 vector2 = VectorUtilities.Normalize(vector - StartPos);
                    double distance = VectorUtilities.GetDistance(StartPos, vector);
                    bool flag3 = Physics.Raycast(StartPos, vector2, (float)distance + 0.5f, RayMasks.DAMAGE_CLIENT);
                    if (!flag3)
                    {
                        Target.layer = layer;
                        Point = vector;
                        return true;
                    }
                }
                Target.layer = layer;
                result = false;
            }
        }
        return result;
    }
}

