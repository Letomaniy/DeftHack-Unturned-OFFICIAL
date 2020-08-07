using SDG.Unturned;




using UnityEngine;

 
public static class DrawUtilities
{
     
    public static bool ShouldRun()
    {
        return Provider.isConnected && !Provider.isLoading && !LoadingUI.isBlocked && !(OptimizationVariables.MainPlayer == null);
    }

 
    public static int GetTextSize(ESPVisual vis, double dist)
    {
        bool flag = !vis.TextScaling;
        int result;
        if (flag)
        {
            result = vis.FixedTextSize;
        }
        else
        {
            bool flag2 = dist > vis.MinTextSizeDistance;
            if (flag2)
            {
                result = vis.MinTextSize;
            }
            else
            {
                int num = vis.MaxTextSize - vis.MinTextSize;
                double num2 = vis.MinTextSizeDistance / num;
                result = vis.MaxTextSize - (int)(dist / num2);
            }
        }
        return result;
    }

     
    public static Vector2[] GetRectangleLines(Camera cam, Bounds b, Color c)
    {
        Vector3[] array = new Vector3[]
        {
                cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
                cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z))
        };
        for (int i = 0; i < array.Length; i++)
        {
            array[i].y = Screen.height - array[i].y;
        }
        Vector3 vector = array[0];
        Vector3 vector2 = array[0];
        for (int j = 1; j < array.Length; j++)
        {
            vector = Vector3.Min(vector, array[j]);
            vector2 = Vector3.Max(vector2, array[j]);
        }
        return new Vector2[]
        {
                new Vector2(vector.x, vector.y),
                new Vector2(vector2.x, vector.y),
                new Vector2(vector.x, vector2.y),
                new Vector2(vector2.x, vector2.y)
        };
    }
 
    public static Bounds GetBoundsRecursively(GameObject go)
    {
        Bounds result = default(Bounds);
        Collider[] componentsInChildren = go.GetComponentsInChildren<Collider>();
        for (int i = 0; i < componentsInChildren.Length; i++)
        {
            result.Encapsulate(componentsInChildren[i].bounds);
        }
        return result;
    }

    
    public static Bounds TransformBounds(Transform _transform, Bounds _localBounds)
    {
        Vector3 center = _transform.TransformPoint(_localBounds.center);
        Vector3 extents = _localBounds.extents;
        Vector3 vector = _transform.TransformVector(extents.x, 0f, 0f);
        Vector3 vector2 = _transform.TransformVector(0f, extents.y, 0f);
        Vector3 vector3 = _transform.TransformVector(0f, 0f, extents.z);
        extents.x = Mathf.Abs(vector.x) + Mathf.Abs(vector2.x) + Mathf.Abs(vector3.x);
        extents.y = Mathf.Abs(vector.y) + Mathf.Abs(vector2.y) + Mathf.Abs(vector3.y);
        extents.z = Mathf.Abs(vector.z) + Mathf.Abs(vector2.z) + Mathf.Abs(vector3.z);
        return new Bounds
        {
            center = center,
            extents = extents
        };
    }
 
    public static void DrawTextWithOutline(Rect centerRect, string text, GUIStyle style, Color innerColor, Color borderColor, int borderWidth, string outlineText = null)
    {
        bool flag = outlineText == null;
        if (flag)
        {
            outlineText = text;
        }
        style.normal.textColor = borderColor;
        Rect position = centerRect;
        position.x -= borderWidth;
        position.y -= borderWidth;
        GUI.Label(position, text, style);
        while (position.x <= centerRect.x + borderWidth)
        {
            float num = position.x;
            position.x = num + 1f;
            GUI.Label(position, outlineText, style);
        }
        while (position.y <= centerRect.y + borderWidth)
        {
            float num = position.y;
            position.y = num + 1f;
            GUI.Label(position, outlineText, style);
        }
        while (position.x >= centerRect.x - borderWidth)
        {
            float num = position.x;
            position.x = num - 1f;
            GUI.Label(position, outlineText, style);
        }
        while (position.y >= centerRect.y - borderWidth)
        {
            float num = position.y;
            position.y = num - 1f;
            GUI.Label(position, outlineText, style);
        }
        style.normal.textColor = innerColor;
        GUI.Label(centerRect, text, style);
    }
     
    public static Vector2 InvertScreenSpace(Vector2 dim)
    {
        return new Vector2(dim.x, Screen.height - dim.y);
    }
     
    public static string ColorToHex(Color32 color)
    {
        return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
    }
     
    public static void DrawLabel(Font Font, LabelLocation Location, Vector2 W2SVector, string Content, Color BorderColor, Color InnerColor, int BorderWidth, string outerContent = null, int fontSize = 12)
    {
        GUIContent guicontent = new GUIContent(Content);
        GUIStyle guistyle = new GUIStyle
        {
            font = Font,
            fontSize = fontSize
        };
        Vector2 vector = guistyle.CalcSize(guicontent);
        float x = vector.x;
        float y = vector.y;
        Rect centerRect = new Rect(0f, 0f, x, y);
        switch (Location)
        {
            case LabelLocation.TopRight:
                centerRect.x = W2SVector.x;
                centerRect.y = W2SVector.y - y;
                guistyle.alignment = TextAnchor.UpperLeft;
                break;
            case LabelLocation.TopMiddle:
                centerRect.x = W2SVector.x - x / 2f;
                centerRect.y = W2SVector.y - y;
                guistyle.alignment = TextAnchor.LowerCenter;
                break;
            case LabelLocation.TopLeft:
                centerRect.x = W2SVector.x - x;
                centerRect.y = W2SVector.y - y;
                guistyle.alignment = TextAnchor.UpperRight;
                break;
            case LabelLocation.MiddleRight:
                centerRect.x = W2SVector.x;
                centerRect.y = W2SVector.y - y / 2f;
                guistyle.alignment = TextAnchor.MiddleLeft;
                break;
            case LabelLocation.Center:
                centerRect.x = W2SVector.x - x / 2f;
                centerRect.y = W2SVector.y - y / 2f;
                guistyle.alignment = TextAnchor.MiddleCenter;
                break;
            case LabelLocation.MiddleLeft:
                centerRect.x = W2SVector.x - x;
                centerRect.y = W2SVector.y - y / 2f;
                guistyle.alignment = TextAnchor.MiddleRight;
                break;
            case LabelLocation.BottomRight:
                centerRect.x = W2SVector.x;
                centerRect.y = W2SVector.y;
                guistyle.alignment = TextAnchor.LowerLeft;
                break;
            case LabelLocation.BottomMiddle:
                centerRect.x = W2SVector.x - x / 2f;
                centerRect.y = W2SVector.y;
                guistyle.alignment = TextAnchor.UpperCenter;
                break;
            case LabelLocation.BottomLeft:
                centerRect.x = W2SVector.x - x;
                centerRect.y = W2SVector.y;
                guistyle.alignment = TextAnchor.LowerRight;
                break;
        }
        bool flag = centerRect.x - 10f < 0f || centerRect.y - 10f < 0f;
        if (!flag)
        {
            bool flag2 = centerRect.x + 10f > Screen.width || centerRect.y + 10f > Screen.height;
            if (!flag2)
            {
                DrawUtilities.DrawTextWithOutline(centerRect, guicontent.text, guistyle, BorderColor, InnerColor, BorderWidth, outerContent);
            }
        }
    }
     
    public static Vector2 Get3DW2SVector(Camera cam, Bounds b, LabelLocation location)
    {
        Vector2 result;
        switch (location)
        {
            case LabelLocation.TopRight:
            case LabelLocation.TopMiddle:
            case LabelLocation.TopLeft:
                result = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(new Vector3(b.center.x, b.center.y + b.extents.y, b.center.z)));
                break;
            case LabelLocation.MiddleRight:
            case LabelLocation.Center:
            case LabelLocation.MiddleLeft:
                result = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(b.center));
                break;
            case LabelLocation.BottomRight:
            case LabelLocation.BottomMiddle:
            case LabelLocation.BottomLeft:
                result = DrawUtilities.InvertScreenSpace(cam.WorldToScreenPoint(new Vector3(b.center.x, b.center.y - b.extents.y, b.center.z)));
                break;
            default:
                result = Vector2.zero;
                break;
        }
        return result;
    }
     
    public static Vector2 Get2DW2SVector(Camera cam, Vector2[] Corners, LabelLocation location)
    {
        Vector2 result;
        switch (location)
        {
            case LabelLocation.TopRight:
                result = Corners[1];
                break;
            case LabelLocation.TopMiddle:
                result = new Vector2((Corners[0].x + Corners[1].x) / 2f, Corners[0].y);
                break;
            case LabelLocation.TopLeft:
                result = Corners[0];
                break;
            case LabelLocation.MiddleRight:
                result = new Vector2(Corners[0].x, (Corners[1].y + Corners[2].y) / 2f);
                break;
            case LabelLocation.Center:
                result = new Vector2(Corners[2].x, (Corners[1].y + Corners[2].y) / 2f);
                break;
            case LabelLocation.MiddleLeft:
                result = new Vector2((Corners[2].x + Corners[3].x) / 2f, (Corners[1].y + Corners[2].y) / 2f);
                break;
            case LabelLocation.BottomRight:
                result = Corners[2];
                break;
            case LabelLocation.BottomMiddle:
                result = new Vector2((Corners[2].x + Corners[3].x) / 2f, Corners[2].y);
                break;
            case LabelLocation.BottomLeft:
                result = Corners[3];
                break;
            default:
                result = Vector2.zero;
                break;
        }
        return result;
    }
     
    public static Vector3[] GetBoxVectors(Bounds b)
    {
        Vector3 center = b.center;
        Vector3 extents = b.extents;
        return new Vector3[]
        {
                new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z),
                new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z),
                new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z),
                new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z),
                new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z),
                new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z),
                new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z),
                new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z)
        };
    }
     
    public static void PrepareRectangleLines(Vector2[] nvectors, Color c)
    {
        ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
        {
            Color = c,
            Vertices = new Vector2[]
            {
                    nvectors[0],
                    nvectors[1],
                    nvectors[1],
                    nvectors[3],
                    nvectors[3],
                    nvectors[2],
                    nvectors[2],
                    nvectors[0]
            }
        });
    }
     
    public static void PrepareBoxLines(Vector3[] vectors, Color c)
    {
        ESPVariables.DrawBuffer.Enqueue(new ESPBox
        {
            Color = c,
            Vertices = new Vector3[]
            {
                    vectors[0],
                    vectors[1],
                    vectors[1],
                    vectors[3],
                    vectors[3],
                    vectors[2],
                    vectors[2],
                    vectors[0],
                    vectors[4],
                    vectors[5],
                    vectors[5],
                    vectors[7],
                    vectors[7],
                    vectors[6],
                    vectors[6],
                    vectors[4],
                    vectors[0],
                    vectors[4],
                    vectors[1],
                    vectors[5],
                    vectors[2],
                    vectors[6],
                    vectors[3],
                    vectors[7]
            }
        });
    }
     
    public static void DrawCircle(Material Mat, Color Col, Vector2 Center, float Radius)
    {
        GL.PushMatrix();
        Mat.SetPass(0);
        GL.Begin(1);
        GL.Color(Col);
        for (float num = 0f; num < 6.28318548f; num += 0.01f)
        {
            Vector2 v = new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y);
            GL.Vertex(v);
        }
        GL.End();
        GL.PopMatrix();
    }
     
    public static void DrawMenuRect(float x, float y, float width, float height, Color fillcolor)
    {
        Color black = Color.black;
        Drawing.DrawRect(new Rect(x, y, width, 5f), black, null);
        Drawing.DrawRect(new Rect(x, y, 5f, height), black, null);
        Drawing.DrawRect(new Rect(x, y + (height - 5f), width, 5f), black, null);
        Drawing.DrawRect(new Rect(x + (width - 5f), 0f, 5f, height), black, null);
        Drawing.DrawRect(new Rect(5f, 5f, width - 10f, height - 10f), fillcolor, null);
    }
}