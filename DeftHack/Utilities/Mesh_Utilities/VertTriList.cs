using UnityEngine;

 
public class VertTriList
{ 
    public int[] this[int index] => list[index];

    
    public VertTriList(int[] tri, int numVerts)
    {
        Init(tri, numVerts);
    }

 
    public VertTriList(Mesh mesh)
    {
        Init(mesh.triangles, mesh.vertexCount);
    }

    
    public void Init(int[] tri, int numVerts)
    {
        int[] array = new int[numVerts];
        for (int i = 0; i < tri.Length; i++)
        {
            array[tri[i]]++;
        }
        list = new int[numVerts][];
        for (int j = 0; j < array.Length; j++)
        {
            list[j] = new int[array[j]];
        }
        for (int k = 0; k < tri.Length; k++)
        {
            int num = tri[k];
            int[] array2 = list[num];
            int[] array3 = array;
            int num2 = num;
            int num3 = array3[num2] - 1;
            array3[num2] = num3;
            array2[num3] = k / 3;
        }
    }

 
    public int[][] list;
}

