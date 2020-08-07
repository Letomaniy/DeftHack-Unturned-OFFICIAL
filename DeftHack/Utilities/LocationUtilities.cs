using SDG.Unturned;
using System.Linq;
using UnityEngine;

 
public static class LocationUtilities
{ 
    public static LocationNode GetClosestLocation(Vector3 pos)
    {
        double num = 1337420.0;
        LocationNode result = null;
        foreach (LocationNode locationNode in (from n in LevelNodes.nodes
                                               where n.type == ENodeType.LOCATION
                                               select (LocationNode)n).ToArray<LocationNode>())
        {
            double distance = VectorUtilities.GetDistance(pos, locationNode.point);
            bool flag = distance < num;
            if (flag)
            {
                num = distance;
                result = locationNode;
            }
        }
        return result;
    }
}

