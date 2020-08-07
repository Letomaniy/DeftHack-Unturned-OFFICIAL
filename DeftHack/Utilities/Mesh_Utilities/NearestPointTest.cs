using UnityEngine;


 public static class NearestPointTest
{ 
    public static Vector3 NearestPointOnMesh(Vector3 pt, Vector3[] verts, int[] tri, VertTriList vt)
    {
        int num = -1;
        float num2 = 1E+08f;
        for (int i = 0; i < verts.Length; i++)
        {
            float sqrMagnitude = (verts[i] - pt).sqrMagnitude;
            bool flag = sqrMagnitude >= num2;
            if (!flag)
            {
                num = i;
                num2 = sqrMagnitude;
            }
        }
        bool flag2 = num == -1;
        Vector3 result;
        if (flag2)
        {
            result = Vector3.zero;
        }
        else
        {
            int[] array = vt[num];
            Vector3 vector = Vector3.zero;
            num2 = 1E+08f;
            for (int j = 0; j < array.Length; j++)
            {
                int num3 = array[j] * 3;
                Vector3 vector2 = verts[tri[num3]];
                Vector3 vector3 = verts[tri[num3 + 1]];
                Vector3 vector4 = verts[tri[num3 + 2]];
                Vector3 vector5 = NearestPointTest.NearestPointOnTri(pt, vector2, vector3, vector4);
                float sqrMagnitude2 = (pt - vector5).sqrMagnitude;
                bool flag3 = sqrMagnitude2 >= num2;
                if (!flag3)
                {
                    vector = vector5;
                    num2 = sqrMagnitude2;
                }
            }
            result = vector;
        }
        return result;
    }
     
    public static Vector3 NearestPointOnTri(Vector3 pt, Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 vector = b - a;
        Vector3 vector2 = c - a;
        Vector3 vector3 = c - b;
        float magnitude = vector.magnitude;
        float magnitude2 = vector2.magnitude;
        float magnitude3 = vector3.magnitude;
        Vector3 vector4 = pt - a;
        Vector3 vector5 = pt - b;
        Vector3 vector6 = pt - c;
        Vector3 vector7 = vector / magnitude;
        Vector3 normalized = Vector3.Cross(vector, vector2).normalized;
        Vector3 vector8 = Vector3.Cross(normalized, vector7);
        Vector3 vector9 = Vector3.Cross(vector, vector4);
        Vector3 vector10 = Vector3.Cross(vector2, -vector6);
        Vector3 vector11 = Vector3.Cross(vector3, vector5);
        bool flag = Vector3.Dot(vector9, normalized) > 0f;
        bool flag2 = Vector3.Dot(vector10, normalized) > 0f;
        bool flag3 = Vector3.Dot(vector11, normalized) > 0f;
        bool flag4 = flag && flag2 && flag3;
        Vector3 result;
        if (flag4)
        {
            float num = Vector3.Dot(vector4, vector7);
            float num2 = Vector3.Dot(vector4, vector8);
            result = a + vector7 * num + vector8 * num2;
        }
        else
        {
            Vector3 vector12 = vector7;
            Vector3 normalized2 = vector2.normalized;
            Vector3 normalized3 = vector3.normalized;
            float num3 = Mathf.Clamp(Vector3.Dot(vector12, vector4), 0f, magnitude);
            float num4 = Mathf.Clamp(Vector3.Dot(normalized2, vector4), 0f, magnitude2);
            float num5 = Mathf.Clamp(Vector3.Dot(normalized3, vector5), 0f, magnitude3);
            Vector3 vector13 = a + num3 * vector12;
            Vector3 vector14 = a + num4 * normalized2;
            Vector3 vector15 = b + num5 * normalized3;
            float sqrMagnitude = (pt - vector13).sqrMagnitude;
            float sqrMagnitude2 = (pt - vector14).sqrMagnitude;
            float sqrMagnitude3 = (pt - vector15).sqrMagnitude;
            result = ((sqrMagnitude < sqrMagnitude2) ? ((sqrMagnitude < sqrMagnitude3) ? vector13 : vector15) : ((sqrMagnitude2 < sqrMagnitude3) ? vector14 : vector15));
        }
        return result;
    }
     
    public static Vector3 a;
     
    public static Vector3 b;
     
    public static Vector3 c;
     
    public static Vector3 pt;
}

