using UnityEngine;


 
public class SerializableVector2
{ 
    public SerializableVector2(float nx, float ny)
    {
        x = nx;
        y = ny;
    }
     
    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
     
    public static implicit operator Vector2(SerializableVector2 vector)
    {
        return vector.ToVector2();
    }
     
    public static implicit operator SerializableVector2(Vector2 vector)
    {
        return new SerializableVector2(vector.x, vector.y);
    }
     
    public float x;
     
    public float y;
}

