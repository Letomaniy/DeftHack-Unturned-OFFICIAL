using UnityEngine;

 
public class SerializableVector
{ 
    public SerializableVector(float nx, float ny, float nz)
    {
        x = nx;
        y = ny;
        z = nz;
    }
     
    public Vector3 ToVector()
    {
        return new Vector3(x, y, z);
    }
     
    public static implicit operator Vector3(SerializableVector vector)
    {
        return vector.ToVector();
    }
     
    public float x;
     
    public float y;
     
    public float z;
}

