using UnityEngine;

 
public class LocalBounds
{
    
    public LocalBounds(Vector3 po, Vector3 e)
    {
        PosOffset = po;
        Extents = e;
    }
     
    public Vector3 PosOffset;
     
    public Vector3 Extents;
}

